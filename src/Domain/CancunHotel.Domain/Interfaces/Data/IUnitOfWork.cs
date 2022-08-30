namespace CancunHotel.Domain.Interfaces.Data;

public interface IUnitOfWork
{
    bool Commit();
    Task<bool> CommitAsync();
}