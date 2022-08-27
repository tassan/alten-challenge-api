using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Data;
using CancunHotel.Domain.Interfaces.Repository;

namespace CancunHotel.Infra.Repository;

public class CustomerRepository : ICustomerRepository
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IUnitOfWork UnitOfWork { get; }
    public Task<Customer> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Add(Customer customer)
    {
        throw new NotImplementedException();
    }

    public void Update(Customer customer)
    {
        throw new NotImplementedException();
    }

    public void Remove(Customer customer)
    {
        throw new NotImplementedException();
    }
}