namespace Discount.Grpc.Localization;

public static class Localizable
{
    public const string DiscountRouteConstant = "api/v1/[controller]";
    public static readonly string DatabaseSettings = "DatabaseSettings:";
    public static readonly string ConnectionString = string.Concat(DatabaseSettings, "ConnectionString");
}