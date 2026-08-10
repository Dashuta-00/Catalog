using Common.Kernel.CQRS.Commands;
using Mapster;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistence.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.DeletePromo;

public class DeletePromoCommandHandler(IPromoRepository promoRepository) 
    : ICommandHandler<DeletePromoCommand, DeletePromoResponse>
{
    public async Task<DeletePromoResponse> Handle(DeletePromoCommand request, CancellationToken cancellationToken)
    {
        var deleteModel = request.Promo.Adapt<DeletePromoModel>();

        var success = await promoRepository.DeleteByCatalogItemIdAsync(deleteModel, cancellationToken);
        
        var result = new DeletePromoResponse
        {
            Success = success,
            Description = success ? "deletion successful" :  "deletion failed"
        };
        
        return result;
    }
}