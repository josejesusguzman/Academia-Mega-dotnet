// Este nos va a servir para el DTO del backend

namespace TaskManagerClient.Models;

public class TaskItem
{
    public Guid id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsDone { get; set; }
}