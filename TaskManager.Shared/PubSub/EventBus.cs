using TaskManager.Shared.Events;

namespace TaskManager.Shared.PubSub;

public class EventBus : IObservable<TaskEvent>
{
    private readonly List<IObserver<TaskEvent>> _observers = [];

    public IDisposable Subscribe(IObserver<TaskEvent> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);

        return new Unsubscriber(_observers, observer);
    }

    public void Publish(TaskEvent ev)
    {
        foreach (var obs in _observers.ToArray())
            obs.OnNext(ev);
    }

    private sealed class Unsubscriber(List<IObserver<TaskEvent>> list, IObserver<TaskEvent> obs)
        : IDisposable
    {
        public void Dispose() => list.Remove(obs);
    }

}