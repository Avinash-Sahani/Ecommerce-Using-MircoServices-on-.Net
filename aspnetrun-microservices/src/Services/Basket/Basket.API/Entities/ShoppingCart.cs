namespace Basket.API.Entities;

public class ShoppingCart
{
    public ShoppingCart(string username)
    {
        Username = username;
    }
    public string Username { get; set; }

    public List<ShoppingCartItem> Items { get; set; } = new();

    public decimal TotalPrice => CalculateCartItemsTotalPrice();

    private decimal CalculateCartItemsTotalPrice()
    {
        return Items.Sum(item => item.ItemTotalPrice);
    }
}