namespace CancunHotel.Domain.Interfaces;

public interface IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Deleted { get; set; }
}