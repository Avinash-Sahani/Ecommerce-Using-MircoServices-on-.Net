using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _client;

    public CatalogService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalog()
    {
        var response = await _client.GetAsync("/api/v1/Catalog/products");
        return await response.ReadContentAs<List<CatalogModel>>() ?? Enumerable.Empty<CatalogModel>();
        
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
    {
        var response = await _client.GetAsync($"/api/v1/Catalog/products/category/{category}");
        return await response.ReadContentAs<List<CatalogModel>>() ?? Enumerable.Empty<CatalogModel>();
    }

    public async Task<CatalogModel> GetCatalog(string id) 
    {
        var response = await _client.GetAsync($"/api/v1/Catalog/products/{id}");
        return await response.ReadContentAs<CatalogModel>() ?? new CatalogModel();
    }
}