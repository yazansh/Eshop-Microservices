using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extensions
{

    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        using var dbContext = serviceProvider.GetRequiredService<DiscountDbContext>();

        dbContext.Database.Migrate();

        return app;
    }
}
