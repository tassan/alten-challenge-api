namespace CancunHotel.Domain.Interfaces.Data;

public interface IRepository : IDisposable
{
    IUnitOfWork UnitOfWork { get; }
}