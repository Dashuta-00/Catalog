using Common.Kernel.CQRS.Commands;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.DeletePromo;

public record DeletePromoCommand(DeletePromoRequest Promo) : ICommand<DeletePromoResponse>;