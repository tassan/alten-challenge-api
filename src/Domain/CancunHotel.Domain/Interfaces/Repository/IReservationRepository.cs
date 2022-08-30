using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Data;

namespace CancunHotel.Domain.Interfaces.Repository;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation> GetByCustomer(Guid customerId);
    IQueryable<Reservation> GetByDates(DateTime checkIn, DateTime checkOut);
}