namespace TaskManager.Models
{
    public abstract class BaseClass
    {
        public Guid id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    }
}