
namespace Discount.API.Localization;

public  static  class Localizable
{
    public static readonly string DatabaseSettings = "DatabaseSettings:";
    public static readonly string ConnectionString = string.Concat(DatabaseSettings,"ConnectionString");
    public const string  DiscountRouteConstant = "api/v1/[controller]";
}