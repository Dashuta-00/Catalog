namespace Promotion.Grpc.Domain;

public class UpdatePromoModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Value { get; set; } 
}