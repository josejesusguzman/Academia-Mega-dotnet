using TaskManagerClient.Models;

namespace TaskManagerClient.Services;

public interface ITaskReader
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
}