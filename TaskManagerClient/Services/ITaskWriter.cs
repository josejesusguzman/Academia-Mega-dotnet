using TaskManagerClient.Models;

namespace TaskManagerClient.Services;

public interface ITaskWriter
{
    Task<TaskItem> AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
}