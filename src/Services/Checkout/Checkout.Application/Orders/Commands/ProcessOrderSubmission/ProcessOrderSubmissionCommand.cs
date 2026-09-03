using Checkout.Application.DTOs;
using Common.Kernel.CQRS.Commands;

namespace Checkout.Application.Orders.Commands.ProcessOrderSubmission;

public record ProcessOrderSubmissionCommand(
    Guid OrderId,
    
    string AccountName,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string City,
    string Region,
    string PostalCode,
    int PaymentMethod,
    string? CardName,
    string? CardNumber,
    string? Expiration,
    string? Cvv,
    string CorrelationId,
    
    decimal TotalPrice,
    List<OrderItemDto> Items) 
    : ICommand<ProcessOrderSubmissionResult>;