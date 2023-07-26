#nullable enable
namespace Shopping.Aggregator.Models;

public class CatalogModel
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? ImageFile { get; set; }
    
    public decimal Price { get; set; }
    
}