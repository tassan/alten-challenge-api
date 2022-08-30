using CancunHotel.Domain.Interfaces;

namespace CancunHotel.Domain.DomainObjects;

public abstract class EntityService : IEntityService<Entity>
{
    public Task<Entity> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Add(Entity entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Entity entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}