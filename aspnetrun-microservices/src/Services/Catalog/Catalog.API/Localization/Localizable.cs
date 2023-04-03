namespace Catalog.API.Localization;

public  static  class Localizable
{
    public static readonly string DatabaseSettings = "DatabaseSettings:";
    public static readonly string ConnectionString = string.Concat(DatabaseSettings,"ConnectionString");
    public static readonly string DatabaseName = string.Concat(DatabaseSettings, "DatabaseName");
    public static readonly string CollectionName = string.Concat(DatabaseSettings,"CollectionName");
    public const string  CatalogRouteConstant = "api/v1/[controller]/products";
}