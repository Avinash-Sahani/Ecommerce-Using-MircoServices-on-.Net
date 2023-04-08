using System.Text.Json.Nodes;
using Basket.API.Entities;
using Newtonsoft.Json;

namespace Basket.API;

public static class JsonHelpers
{
    public static string Serialze(ShoppingCart shoppingCart)
    {
        return JsonConvert.SerializeObject(shoppingCart);
    }

    public static ShoppingCart? Deserialze(string jsonString)
    {
        try
        {
            return JsonConvert.DeserializeObject<ShoppingCart>(jsonString);

        }
        catch (Exception e)
        {
            return null;
        }
    }
}