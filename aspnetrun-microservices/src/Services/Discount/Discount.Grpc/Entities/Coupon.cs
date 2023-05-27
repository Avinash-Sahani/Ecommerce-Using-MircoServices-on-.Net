namespace Discount.Grpc.Entities;

public class Coupon
{
    public static Coupon Default = new()
        { Id = -1, Description = "No Product Found", ProductName = string.Empty, Amount = 0 };

    public int Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;


    public int Amount { get; set; }
}