using System;
using Bogus;
using CancunHotel.Application.ViewModels;
using CancunHotel.Domain.Entities;

namespace CancunHotel.Tests.Fixtures;

public class CustomerFixture : IDisposable
{
    public static Customer CreateCustomer()
    {
        return new Faker<Customer>()
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.BirthDate, f => f.Person.DateOfBirth)
            .Generate();
    }
    
    public static CustomerViewModel CreateCustomerViewModel()
    {
        return new Faker<CustomerViewModel>()
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.BirthDate, f => f.Person.DateOfBirth)
            .Generate();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        
    }
}