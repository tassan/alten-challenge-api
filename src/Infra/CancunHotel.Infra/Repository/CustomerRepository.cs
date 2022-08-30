using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Data;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CancunHotel.Infra.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationContext _context;
    private readonly DbSet<Customer> _dbSet;

    public CustomerRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = _context.Set<Customer>();
    }

    public IUnitOfWork UnitOfWork => _context;
    public async Task<Customer> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<Customer> GetByEmail(string email)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public void Add(Customer customer)
    {
        _dbSet.Add(customer);
    }

    public void Update(Customer customer)
    {
        _dbSet.Update(customer);
    }

    public void Remove(Customer customer)
    {
        _dbSet.Update(customer);
    }
    
    public void Dispose() => _context.Dispose();
}