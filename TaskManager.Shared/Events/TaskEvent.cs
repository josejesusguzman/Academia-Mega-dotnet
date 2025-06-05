using TaskManager.Shared.Domain;

namespace TaskManager.Shared.Events;

public record TaskEvent(string EventName, TaskItem Payload);