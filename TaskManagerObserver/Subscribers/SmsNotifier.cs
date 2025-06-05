using Domain.Events;
using System.Diagnostics;

namespace Subscribers;

public class SmsNotifier : IObserver<TaskEvent>
{
    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(TaskEvent taskEvent)
    {
        if (taskEvent.EventName == "Deleted")
            Console.WriteLine($"[SMS] Tarea eliminada: {taskEvent.Payload.Title}");
    }
}