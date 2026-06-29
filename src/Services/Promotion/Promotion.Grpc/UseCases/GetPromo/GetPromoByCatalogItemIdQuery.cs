using Common.Kernel.CQRS.Queries;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.GetPromo;

public record GetPromoByCatalogItemIdQuery(string CatalogItemId) 
    : IQuery<PromoModel>;