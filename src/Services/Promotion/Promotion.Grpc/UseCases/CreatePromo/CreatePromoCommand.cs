using Common.Kernel.CQRS.Commands;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.CreatePromo;

public record CreatePromoCommand(CreatePromoRequest Promo) : ICommand<CreatePromoResponse>;