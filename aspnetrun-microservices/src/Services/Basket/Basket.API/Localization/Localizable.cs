namespace Catalog.API.Localization;

public  static  class Localizable
{
    public static readonly string RedisSettings = "Redis:";
    public static readonly string CacheUrl = string.Concat(RedisSettings,"CacheUrl");
    public const string BasketRouteConstant = "api/v1/[controller]";
}