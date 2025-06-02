using TaskManager.Models;
using System.Collections.Concurrent;

namespace TaskManager.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {

        private readonly ConcurrentDictionary<Guid, TaskItem> _db = new();

        public Task<IEnumerable<TaskItem>> GetAllAsync() =>
            Task.FromResult(_db.Values.AsEnumerable());

        public Task<TaskItem?> GetAsync(Guid id) =>
            Task.FromResult(_db.GetValueOrDefault(id));

        public Task AddAsync(TaskItem task)
        {
            _db[task.id] = task;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TaskItem task)
        {
            _db[task.id] = task;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _db.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}