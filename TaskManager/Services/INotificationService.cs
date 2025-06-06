using TaskManager.Shared.Domain;

namespace TaskManager.Services
{
    public interface INotificationService
    {
        Task NotifyTaskCreatedAsync(TaskItem taskItem);
    }
}