using System.Net.Http.Json;
using TaskManagerClient.Helpers;
using TaskManagerClient.Models;

namespace TaskManagerClient.Services;

public class TaskService(HttpClient http) : ITaskReader, ITaskWriter
{
    public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
        await http.GetFromJsonAsync<IEnumerable<TaskItem>>(ApiEndpoints.Tasks)
            ?? Enumerable.Empty<TaskItem>();

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        var response = await http.PostAsJsonAsync(ApiEndpoints.Tasks, task);
        return await response.Content.ReadFromJsonAsync<TaskItem>() ??
            throw new InvalidOperationException("Respuesta vacia");
    }

    public Task UpdateAsync(TaskItem task) =>
        http.PutAsJsonAsync($"ApiEndpoints.Tasks/{task.id}", task);

    public Task DeleteAsync(Guid id) =>
        http.DeleteAsync($"ApiEndpoints.Tasks/{id}");

}
