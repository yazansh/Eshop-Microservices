using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors;
public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // I think it is more logical to dispatch and publish events after the save!
        var saveResult = base.SavingChanges(eventData, result);
        DispatchDomanEventsAsync(eventData.Context).GetAwaiter().GetResult();

        return saveResult;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var saveResult = await base.SavingChangesAsync(eventData, result, cancellationToken);
        await DispatchDomanEventsAsync(eventData.Context);

        return saveResult;
    }

    private async Task DispatchDomanEventsAsync(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(ee => ee.Entity.DomainEvents.Any())
            .Select(ee => ee.Entity);

        var domainEvents = aggregates.SelectMany(ee => ee.DomainEvents).ToList();

        aggregates.ToList().ForEach(a => a.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
