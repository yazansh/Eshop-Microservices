﻿namespace Basket.API.Models;

public class ShoppingCartItem
{
    public string? Color { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;
}
