namespace Shopping.Aggregator.Models;
#nullable enable
public class ShoppingModel
{
    
    public string? UserName { get; set; } = string.Empty;
    public BasketModel? BasketWithProducts { get; set; } = new();
    public IEnumerable<OrderResponse>? Orders { get; set; } = Enumerable.Empty<OrderResponse>();

}