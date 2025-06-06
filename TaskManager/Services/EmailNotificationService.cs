using TaskManager.Shared.Domain;

namespace TaskManager.Services
{
    public class EmailNotificationService : NotificationService
    {
        public EmailNotificationService() : base("Gestor de tareas") { }

        public override Task NotifyTaskCreatedAsync(TaskItem task)
        {
            // Imaginate que aquí se manda el correo
            Console.WriteLine($"[EMAIL] {SenderName}: Nueva tarea '{task.Title}'");
            return Task.CompletedTask;
        }

    }
}