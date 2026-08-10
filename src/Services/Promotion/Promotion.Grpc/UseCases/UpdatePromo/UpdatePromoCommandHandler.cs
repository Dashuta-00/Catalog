using Common.Kernel.CQRS.Commands;
using Mapster;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistence.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.UpdatePromo;

public class UpdatePromoCommandHandler(IPromoRepository promoRepository) : ICommandHandler<UpdatePromoCommand,  UpdatePromoResponse>
{
    public async Task<UpdatePromoResponse> Handle(UpdatePromoCommand command, CancellationToken cancellationToken)
    {
        var updatePromoModel = command.Promo.Adapt<UpdatePromoModel>();

        var success = await promoRepository.UpdateAsync(updatePromoModel, cancellationToken);
        
        return new UpdatePromoResponse
        {
            Success = success,
            Description = success ? "Promo updated successfully!" : "Promo update failed!"
        };
    }


}