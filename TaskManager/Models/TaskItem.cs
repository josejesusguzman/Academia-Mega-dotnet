namespace TaskManager.Models
{
    public class TaskItem : BaseClass
    {

        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; private set; }

        public void Complete() => IsDone = true;
    }
}