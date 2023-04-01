using Catalog.API.Entities;

namespace Catalog.API.Repositories;

public interface IProductRepository
{

     public Task CreateProduct(Product product);
     public  Task<bool> UpdateProduct(Product product);
     public   Task<bool> DeleteProduct(string id);

     public Task<IEnumerable<Product>> GetAllProducts();
     public  Task<Product?> GetProduct(string id);
     public  Task<IEnumerable<Product>> GetProductsByName(string name);
     public Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);

}
