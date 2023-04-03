#nullable  enable
using Catalog.API.Entities;
using Catalog.API.Localization;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContext : ICatalogContext
{
    private IConfiguration? Configuration { get; set; }
    public CatalogContext(IConfiguration? config)
    {
        Configuration = config;
        InitializeMongoDb();
        SeedDataInProducts();
    }

    private void SeedDataInProducts()
    {
        CatalogContextSeed.SeedData(Products);
    }

    private void InitializeMongoDb()
    {
        var client = new MongoClient(GetConfigurationString(Localizable.ConnectionString));
        var database = client.GetDatabase(GetConfigurationString(Localizable.DatabaseName));
        Products = database.GetCollection<Product>(GetConfigurationString(Localizable.CollectionName));
    }
    
    private string GetConfigurationString(string configKey)
    {
        return Configuration.GetValue<string>(configKey);
    }


    public IMongoCollection<Product> Products { get; set; } 
}