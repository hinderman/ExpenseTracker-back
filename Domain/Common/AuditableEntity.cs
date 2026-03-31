namespace Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; protected init; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        protected void SetUpdated() => UpdatedAt = DateTime.UtcNow;
        protected void SetDeleted() => DeletedAt = DateTime.UtcNow;
    }
}
