using CancunHotel.Domain.Interfaces;
using CancunHotel.Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace CancunHotel.Infra.Repository;

public abstract class GenericRepository<TEntity, TContext> : IRepository<TEntity>
    where TContext : DbContext, IUnitOfWork
    where TEntity : class, IEntity
{
    protected readonly TContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected GenericRepository(TContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public void Dispose() => Context.Dispose();

    public IUnitOfWork UnitOfWork => Context;

    public virtual async Task<TEntity> GetById(Guid id)
    {
#pragma warning disable CS8603
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
#pragma warning restore CS8603
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public virtual void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }
}