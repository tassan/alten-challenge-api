using CancunHotel.Application.Interfaces;
using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Tests.Fixtures;
using FakeItEasy;
using Xunit;

namespace CancunHotel.Tests.Services;

public class CustomerAppServiceTests
{
    [Fact]
    public void Should_Register_Customer()
    {
        var customerRepository = A.Fake<ICustomerRepository>();
        var customerService = A.Fake<ICustomerAppService>();
        var customerViewModel = CustomerFixture.CreateCustomerViewModel();
        
        customerService.Register(customerViewModel);

        A.CallTo(() => customerRepository.Add(new Customer()))
            .MustHaveHappened();
    }
}