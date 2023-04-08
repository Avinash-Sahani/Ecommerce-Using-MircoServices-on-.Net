namespace Basket.API.Entities;

public class ShoppingCartItem
{
    public int Quantity { get; set; } = 0;

    public string Color { get; set; } = string.Empty;

    public decimal Price { get; set; } = 0;

    public string ProductId { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public decimal ItemTotalPrice => GetItemPrice();

    private decimal GetItemPrice()
    {
        return Price * Quantity;
    }
}