using Common.Kernel.CQRS.Commands;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.UpdatePromo;

public record UpdatePromoCommand(UpdatePromoRequest Promo) : ICommand<UpdatePromoResponse>;