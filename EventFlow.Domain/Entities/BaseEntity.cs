namespace EventFlow.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; protected set; }

        public bool Active { get; protected set; } = true;

        public void Disable()
        {
            Active = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
