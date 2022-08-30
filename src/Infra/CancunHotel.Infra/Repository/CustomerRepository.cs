using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CancunHotel.Infra.Repository;

public class CustomerRepository : GenericRepository<Customer, ApplicationContext>, ICustomerRepository
{
    public CustomerRepository(ApplicationContext context) : base(context)
    {
    }
    
    public async Task<Customer> GetByEmail(string email)
    {
#pragma warning disable CS8603
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
#pragma warning restore CS8603
    }
    
}