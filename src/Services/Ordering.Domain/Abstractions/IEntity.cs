namespace Ordering.Domain.Abstractions
{
    public interface IEntity
    {
        public DateTime? CatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LateModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }

}
