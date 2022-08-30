using System;
using Bogus;
using CancunHotel.Domain.Entities;
using CancunHotel.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace CancunHotel.Tests.Entities;

public class CustomerTest : IClassFixture<CustomerFixture>
{
    [Fact]
    public void Should_Create_Valid_Customer()
    {
        var customer = CustomerFixture.CreateCustomer();
        
        customer.Id
            .GetType()
            .Should()
            .Be(typeof(Guid));

        customer.FirstName
            .Should()
            .NotBeEmpty();
        
        customer.LastName
            .Should()
            .NotBeEmpty();
        
        customer.Email
            .Should()
            .NotBeEmpty();
        
        customer.BirthDate
            .Should()
            .NotBe(new DateTime());
    }

    [Theory]
    [InlineData("Flávio", "Tassan", "ftassan@outlook.com")]
    [InlineData("George", "Washington", null)]
    [InlineData("Winston", "Churchill", "churchill@eng.gov")]
    public void Test_Customer_Constructor(string firstName, string lastName, string email)
    {
        var customer = new Customer(firstName, lastName, email, new Faker().Person.DateOfBirth);
        
        customer.Id
            .GetType()
            .Should()
            .Be(typeof(Guid));

        customer.FirstName
            .Should()
            .NotBeEmpty();
        
        customer.LastName
            .Should()
            .NotBeEmpty();
        
        customer.Email
            .Should()
            .NotBeEmpty();

        customer.BirthDate
            .Should()
            .NotBe(new DateTime());
    }
}