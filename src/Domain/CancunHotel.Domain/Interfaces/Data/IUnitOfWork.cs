namespace CancunHotel.Domain.Interfaces.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}