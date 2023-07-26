namespace Shopping.Aggregator.Models;
#nullable enable
public class BasketModel
{
    public string UserName { get; set; } = string.Empty;
    public List<BasketItemExtendedModel> Items { get; set; } = new ();
    public decimal TotalPrice { get; set; } = 0;
}