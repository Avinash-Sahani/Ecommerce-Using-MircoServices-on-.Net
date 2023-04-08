using Basket.API.Entities;

namespace Basket.API.Repositories;

public interface IBasketRepository
{

    Task<ShoppingCart?> GetBasket(string username);

    Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart);

    Task<bool> DeleteBasket(string username);
}