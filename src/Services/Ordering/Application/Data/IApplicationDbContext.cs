using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data;
public interface IApplicationDbContext
{
    public DbSet<Order> Orders { get; }
    public DbSet<OrderItem> OrderItems { get; }
    public DbSet<Customer> Customers { get; }
    public DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
