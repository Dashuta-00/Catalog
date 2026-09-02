using Common.Messaging.DTOs;

namespace Common.Messaging.Events;

public class OrderSubmittedEvent : BaseIntegrationEvent
{
    public Guid OrderId { get; set; }
    public string AccountName { get; set; } = default!;
    public decimal TotalPrice { get; set; } 
    
    //contact info

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    
    // delivery address

    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    
    //payment info
    public int PaymentMethod { get; set; }
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? Cvv { get; set; }

    public List<OrderItemEventDto> Items { get; set; } = new();
}