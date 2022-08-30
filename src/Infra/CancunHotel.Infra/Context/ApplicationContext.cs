using CancunHotel.Domain.DomainObjects;
using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace CancunHotel.Infra.Context;

public class ApplicationContext : DbContext, IUnitOfWork
{
    public DbSet<Customer> Customers { get; set; }

    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
    }

    public async Task<bool> Commit()
    {
        var success = await SaveChangesAsync() > 0;
        return success;
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Entity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((Entity) entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((Entity) entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }
}