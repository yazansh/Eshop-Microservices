﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(orderItemId => orderItemId.Value,
                dbId => OrderItemId.Of(dbId));

        // ---- this will be configured in the order configuration ----
        //builder.HasOne<Order>()
        //    .WithMany(order => order.OrderItems)
        //    .HasForeignKey(x => x.OrderId);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId);

        builder.Property(x => x.Quantity).IsRequired();

        builder.Property(x => x.Price).IsRequired();
    }
}
