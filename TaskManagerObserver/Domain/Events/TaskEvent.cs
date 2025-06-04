namespace Domain.Events;

public record TaskEvent(string EventName, TaskItem Payload);