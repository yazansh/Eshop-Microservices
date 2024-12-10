using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;
public class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validatorsResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)).ToList());

        var failures = validatorsResult.Where(vr => vr.Errors.Count != 0).SelectMany(v => v.Errors).ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next();
    }
}