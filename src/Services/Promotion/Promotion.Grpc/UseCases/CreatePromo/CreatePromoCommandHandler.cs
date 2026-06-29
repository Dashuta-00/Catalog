using Common.Kernel.CQRS.Commands;
using Mapster;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistence.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.CreatePromo;

public class CreatePromoCommandHandler(IPromoRepository promoRepository)
: ICommandHandler<CreatePromoCommand, CreatePromoResponse>
{
    public async Task<CreatePromoResponse> Handle(CreatePromoCommand command, CancellationToken cancellationToken)
    {
        var promo = command.Promo.Adapt<Promo>();

        var success = await promoRepository.CreateAsync(promo);

        var result = new CreatePromoResponse
        {
            Id = promo.Id.ToString(),
            Success = success,
            Description = success ? "There is success to create promo" : "There is no success to create promo"
        };
        
        return result;
    }
}