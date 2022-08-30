using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Data;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CancunHotel.Infra.Repository;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationContext _context;
    private readonly DbSet<Reservation> _dbSet;

    public ReservationRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = _context.Set<Reservation>();
    }

    public IUnitOfWork UnitOfWork => _context;
    public async Task<Reservation> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<Reservation> GetByCustomer(Guid customerId)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(r => r.CustomerId == customerId);
    }

    public async Task<IEnumerable<Reservation>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public void Add(Reservation reservation)
    {
        _dbSet.Add(reservation);
    }

    public void Update(Reservation reservation)
    {
        _dbSet.Update(reservation);
    }

    public void Remove(Reservation reservation)
    {
        _dbSet.Update(reservation);
    }
    
    public void Dispose() => _context.Dispose();
}