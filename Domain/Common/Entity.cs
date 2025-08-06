namespace Domain.Common
{
    /// <summary>
    /// Base class for domain entities
    /// </summary>
    public abstract class Entity : IAuditable , ISoftDeleteable
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
    }
}
