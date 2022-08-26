using System;
using CancunHotel.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace CancunHotel.Test.Entities;

public class CustomerTest : IClassFixture<CustomerFixture>
{
    private readonly CustomerFixture _fixture;

    public CustomerTest(CustomerFixture fixture)
    {
        _fixture = fixture;
    }

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
            .NotBeNullOrEmpty();
        
        customer.LastName
            .Should()
            .NotBeNullOrEmpty();
        
        customer.Email
            .Should()
            .NotBeNullOrEmpty();
    }
}