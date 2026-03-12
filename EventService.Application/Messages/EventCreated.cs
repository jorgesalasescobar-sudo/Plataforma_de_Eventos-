namespace EventService.Application.Messages
{
    public record EventCreated(
        Guid MessageId,
        Guid EventId,
        string Name,
        DateTime OccurredAt,
        Guid CorrelationId,
        int Version
    );
}
