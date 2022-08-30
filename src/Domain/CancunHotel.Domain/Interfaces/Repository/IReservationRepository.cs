using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Data;

namespace CancunHotel.Domain.Interfaces.Repository;

public interface IReservationRepository : IRepository
{
    Task<Reservation> GetById(Guid id);
    Task<Reservation> GetByCustomer(Guid customerId);
    Task<IEnumerable<Reservation>> GetAll();

    void Add(Reservation reservation);
    void Update(Reservation reservation);
    void Remove(Reservation reservation);
}