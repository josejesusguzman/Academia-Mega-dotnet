using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Repositories;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repo;
        private readonly IEnumerable<INotificationService> _notifiers;

        public TasksController(ITaskRepository repo, IEnumerable<INotificationService> notifiers)
        {
            _repo = repo;
            _notifiers = notifiers;
        }

        [HttpGet] // GET /api/tasks
        public async Task<IEnumerable<TaskItem>> GetAll() =>
            await _repo.GetAllAsync();

        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create(TaskItem taskItem)
        {
            await _repo.AddAsync(taskItem);

            // Notificar por SMS
            foreach (var notifier in _notifiers)
                await notifier.NotifyTaskCreatedAsync(taskItem);

            return CreatedAtAction(nameof(GetAll), new { id = taskItem.id }, taskItem);

        }

    }
}