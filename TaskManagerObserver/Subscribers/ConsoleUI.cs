using Domain.Events;
using System.Diagnostics;

namespace Subscribers;

public class ConsoleUI : IObserver<TaskEvent>
{
    public void OnCompleted() { }

    public void OnError(Exception error) =>
        Console.WriteLine($"Error: {error.Message}");

    public void OnNext(TaskEvent taskEvent) =>
        Console.WriteLine($"UI -> {taskEvent.EventName}: {taskEvent.Payload.Title}");
}