namespace Promotion.Grpc.Domain;

public class Promo
{
    public Guid Id { get; set; }
    public string? CatalogItemId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Value { get; set; } 
}