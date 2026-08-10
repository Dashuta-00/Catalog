using System.Text.Json.Serialization;

namespace Promotion.Grpc.Domain;

public class DeletePromoModel
{
    [JsonPropertyName("catalogItemId")]
    public Guid CatalogItemId { get; set; }
}