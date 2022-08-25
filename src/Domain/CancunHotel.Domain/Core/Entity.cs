using CancunHotel.Domain.Interfaces;

namespace CancunHotel.Domain.Core;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool Deleted { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}