using Checkout.Application.Orders.Commands.ProcessOrderSubmission;
using Checkout.Domain.Enums;
using Checkout.Domain.Orders;
using Checkout.Domain.ValueObjects;
using Mapster;

namespace Checkout.Application.Mapping;

public class ProcessOrderSubmissionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProcessOrderSubmissionCommand, Order>()
            .Map(dest => dest.Id, src => src.OrderId)
            .Map(dest => dest.TotalAmount, src => src.TotalPrice)
            .Map(dest => dest.CurrentOrderStatus, src => OrderStatus.Submitted)
            .Map(dest => dest.CurrentPaymentMethod, src => (PaymentMethod)src.PaymentMethod)
            .Map(dest => dest.CurrentPaymentStatus, src => PaymentStatus.Pending)
            .Map(dest => dest.ContactInfo, src => new Contact(
                src.FirstName, src.LastName, src.Email))
            .Map(dest => dest.DeliveryAddress, src => new Address(
                src.Street, src.City, src.Region, src.PostalCode))
            .Map(dest => dest.CardDetails, src => !string.IsNullOrEmpty(src.CardName)
            ? new CardDetails(src.CardName, src.CardNumber ?? "", src.Expiration ?? "", src.Cvv ?? "")
            : null)
            .Map(dest => dest.Items, src => src.Items);
    }
}