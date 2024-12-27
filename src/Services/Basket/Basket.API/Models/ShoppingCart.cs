namespace Basket.API.Models;

public class ShoppingCart
{
    public required string Username { get; set; }

    public List<ShoppingCartItem> Items { get; set; } = [];

    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

    [SetsRequiredMembers]
    public ShoppingCart(string userName)
    {
        Username = userName;
    }

    public ShoppingCart()
    {
    }
}
