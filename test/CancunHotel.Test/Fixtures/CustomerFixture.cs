using System;
using Bogus;
using CancunHotel.Domain.Entities;

namespace CancunHotel.Test.Fixtures;

public class CustomerFixture : IDisposable
{
    public static Customer CreateCustomer()
    {
        return new Faker<Customer>()
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .Generate();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}