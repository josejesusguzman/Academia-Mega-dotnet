using Domain;
using Domain.Events;

namespace Intraestructure;

public class TaskRepository
{
    private readonly List<TaskItem> _store = [];
    private readonly EventBus _bus;

    public TaskRepository(EventBus bus) => _bus = bus;

    public IEnumerable<TaskItem> GetAll() => _store.AsReadOnly();

    public TaskItem Add(TaskItem task)
    {
        _store.Add(task);
        _bus.Publish(new TaskEvent("Created", task));
        return task;
    }

    public void Update(TaskItem task)
    {
        var idx = _store.FindIndex(t => t.id == task.id);
        if (idx >= 0)
            _store[idx] = task;
        _bus.Publish(new TaskEvent("Updated", task));
    }

    public void Delete(Guid id)
    {
        var task = _store.FirstOrDefault(t => t.id == id);
        if (task is null)
            return;
        _store.Remove(task);
        _bus.Publish(new TaskEvent("Deleted", task));
    }

}