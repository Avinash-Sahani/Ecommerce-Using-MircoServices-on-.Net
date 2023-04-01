namespace Catalog.API.Localization;

public  static  class Localizable
{
    public static string DatabaseSettings = "DatabaseSettings:";
    public static string ConnectionString = string.Concat(DatabaseSettings,"ConnectionString") ;
    public static string DatabaseName = string.Concat(DatabaseSettings, "DatabaseName");
    public static string CollectionName = string.Concat(DatabaseSettings,"CollectionName");
}