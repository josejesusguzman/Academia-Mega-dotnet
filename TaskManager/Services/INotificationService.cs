using TaskManager.Models;

namespace TaskManager.Services
{
    public interface INotificationService
    {
        Task NotifyTaskCreatedAsync(TaskItem taskItem);
    }
}