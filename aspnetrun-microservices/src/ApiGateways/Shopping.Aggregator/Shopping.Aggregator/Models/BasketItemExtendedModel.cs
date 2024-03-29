namespace Shopping.Aggregator.Models;

public class BasketItemExtendedModel
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string? ProductName { get; set; }

    //Product Related Additional Fields
    public string? Category { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? ImageFile { get; set; }

    public void UpdateBasketItemDataFromCatalog(CatalogModel catalogModel)
    {
        Category = catalogModel.Category;
        Summary = catalogModel.Summary;
        Description = catalogModel.Description;
        ImageFile = catalogModel.ImageFile;
    }
}