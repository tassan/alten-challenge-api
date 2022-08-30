namespace CancunHotel.Domain.Interfaces;

public interface IEntityService<TEntity> where TEntity : IEntity
{
    Task<TEntity> Get(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(Guid id);
}