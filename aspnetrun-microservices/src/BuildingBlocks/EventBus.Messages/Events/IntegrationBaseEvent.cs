namespace EventBus.Messages.Events;

public class IntegrationBaseEvent
{
    public Guid Id { get; private set; }
    private DateTime CreationDate { get; set; }

    public IntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(Guid id, DateTime creationDate)
    {
        Id = id;
        CreationDate = creationDate;
    }
}