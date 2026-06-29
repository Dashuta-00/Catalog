using Mapster;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.Configuration;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<CreatePromoRequest, Promo>.NewConfig()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Value, src => (decimal)src.Value);
        
        TypeAdapterConfig<UpdatePromoRequest, UpdatePromoModel>.NewConfig()
            .Map(dest => dest.Id, src => Guid.Parse(src.Id))
            .Map(dest => dest.Value, src => (decimal)src.Value);

        TypeAdapterConfig<DeletePromoRequest, DeletePromoModel>.NewConfig()
            .Map(dest => dest.CatalogItemId, src => Guid.Parse(src.CatalogItemId));
    }
}