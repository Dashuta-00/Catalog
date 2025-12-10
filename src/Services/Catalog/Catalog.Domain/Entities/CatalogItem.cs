namespace Catalog.Domain.Entities;

public class CatalogItem : BaseEntity
{
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public string? ImageUrl { get; set; }
    public Brand? Brand { get; set; }
    public Category? Category { get; set; }
    public decimal Price { get; set; }
}