using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CancunHotel.Infra.Repository;

public class ReservationRepository : GenericRepository<Reservation, ApplicationContext>, IReservationRepository
{
    public ReservationRepository(ApplicationContext context) : base(context)
    {
    }

    public override void Update(Reservation entity)
    {
        var reservation = GetById(entity.Id).Result;
        DbSet.Attach(entity);
        Context.Entry(entity).CurrentValues.SetValues(reservation);
    }

    public async Task<Reservation> GetByCustomer(Guid customerId)
    {
#pragma warning disable CS8603
        return await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(r => r.CustomerId == customerId);
#pragma warning restore CS8603
    }

    public IQueryable<Reservation> GetByDates(DateTime checkIn, DateTime checkOut)
    {
        var query = DbSet.AsNoTracking()
            .Where(r => r.CheckOutDate.ToLocalTime() >= checkIn && r.CheckInDate.ToLocalTime() <= checkOut);

        return query;
    }
}