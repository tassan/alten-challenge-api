using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Data;

namespace CancunHotel.Domain.Interfaces.Repository;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> GetByEmail(string email);
}