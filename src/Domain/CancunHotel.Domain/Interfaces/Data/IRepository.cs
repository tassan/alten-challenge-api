namespace CancunHotel.Domain.Interfaces.Data;

public interface IRepository<TEntity> : IDisposable
    where TEntity : IEntity
{
    IUnitOfWork UnitOfWork { get; }
    
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();

    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}