#nullable enable
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private ICatalogContext Context { get; }
    public ProductRepository(ICatalogContext context)
    {
        Context = context;
    }
    public async Task CreateProduct(Product product)
    {
        await Context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var productToUpdate =Builders<Product>.Filter.Eq(p =>p.Id,product.Id);
        var updatedResult = await Context.Products.ReplaceOneAsync(productToUpdate, product);
        return IsUpdated();

        bool IsUpdated() => updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var productToDelete =Builders<Product>.Filter.Eq(product =>product.Id ,id);
        var deleteResult = await Context.Products.DeleteOneAsync(productToDelete);
        return IsDeleted();

        bool IsDeleted()
        {
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await Context.Products.Find(product => true).ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await Context.Products.Find(product => product.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        return await Context.Products.Find(product => product.Name.Equals(name)).ToListAsync();

    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
    {
        return await Context.Products.Find(product => product.Category.Equals(categoryName)).ToListAsync();

    }
}