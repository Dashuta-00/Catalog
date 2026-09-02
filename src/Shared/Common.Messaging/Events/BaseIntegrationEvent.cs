namespace Common.Messaging.Events;

public abstract class BaseIntegrationEvent
{
    public string CorrelationId { get; set; } = default!;
    public DateTime CreationDate { get; private set; }
    public string EventType { get; set; } = default!;

    protected BaseIntegrationEvent()
    {
        CorrelationId = Guid.NewGuid().ToString();
        CreationDate = DateTime.UtcNow;
        EventType = GetType().Name;
    }
    
    protected BaseIntegrationEvent(Guid correlationId, DateTime creationDate, string eventType)
    {
        CorrelationId = correlationId.ToString();
        CreationDate = creationDate;
        EventType = eventType;
    }
}