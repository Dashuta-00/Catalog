using System.Text.Json.Serialization;

namespace Promotion.Grpc.Domain;

public class DeletePromoModel
{
    [JsonPropertyName("catalogItemId")]
    public string CatalogItemId { get; set; } = string.Empty;
}