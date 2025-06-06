using System.Collections.Concurrent;
using TaskManager.Shared.PubSub;
using TaskManager.Shared.Events;
using TaskManager.Shared.Domain;
using TaskManager.Hubs;

using Microsoft.AspNetCore.SignalR;

namespace TaskManager.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {

        private readonly ConcurrentDictionary<Guid, TaskItem> _db = new();
        private readonly EventBus _bus;
        private readonly IHubContext<TaskEventHub> _hub;

        public InMemoryTaskRepository(EventBus bus, IHubContext<TaskEventHub> hub)
        {
            _bus = bus;
            _hub = hub;
        }

        public Task<IEnumerable<TaskItem>> GetAllAsync() =>
            Task.FromResult(_db.Values.AsEnumerable());

        public Task<TaskItem?> GetAsync(Guid id) =>
            Task.FromResult(_db.GetValueOrDefault(id));

        public Task AddAsync(TaskItem task)
        {
            _db[task.id] = task;
            var ev = new TaskEvent("Created", task);
            _bus.Publish(ev);
            return _hub.Clients.All.SendAsync("TaskEvent", ev);
        }

        public Task UpdateAsync(TaskItem task)
        {
            _db[task.id] = task;
            var ev = new TaskEvent("Updated", task);
            _bus.Publish(ev);
            return _hub.Clients.All.SendAsync("TaskEvent", ev);
        }

        public Task DeleteAsync(Guid id)
        {
            if (_db.TryRemove(id, out var removed))
            {
                var ev = new TaskEvent("Deleted", removed);
                _bus.Publish(ev);
                return _hub.Clients.All.SendAsync("TaskEvent", ev);
            }
            return Task.CompletedTask;
        }
    }
}