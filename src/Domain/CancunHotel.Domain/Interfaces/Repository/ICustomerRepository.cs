using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Data;

namespace CancunHotel.Domain.Interfaces.Repository;

public interface ICustomerRepository : IRepository
{
    Task<Customer> GetById(Guid id);
    Task<Customer> GetByEmail(string email);
    Task<IEnumerable<Customer>> GetAll();

    void Add(Customer customer);
    void Update(Customer customer);
    void Remove(Customer customer);
}