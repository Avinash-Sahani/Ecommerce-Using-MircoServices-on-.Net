namespace Discount.API.Entities;

public class Coupon
{
    public int Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

   
    public int Amount { get; set; } = 0;

    public static Coupon Default = new()
        { Id = -1, Description = "No Product Found", ProductName = string.Empty, Amount = 0 };
}