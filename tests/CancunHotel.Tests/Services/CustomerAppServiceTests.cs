using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CancunHotel.Application.Services;
using CancunHotel.Application.ViewModels;
using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Tests.Fixtures;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Xunit;
using Times = Moq.Times;

namespace CancunHotel.Tests.Services;

public class CustomerAppServiceTests
{
    /*
     * Test Create Customer
     * Test Update Customer
     * Test Delete Customer
     * Test Get Customer
     */

    [Fact]
    public async Task Given_ValidCustomer_Should_Register_Customer()
    {
        var customerViewModel = CustomerFixture.CreateCustomerViewModel();
        var customer = new Customer(customerViewModel.FirstName,
            customerViewModel.LastName,
            customerViewModel.Email,
            customerViewModel.BirthDate);

        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<CustomerAppService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
            .Returns(customer);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        var result = await service.Register(customerViewModel);

        Assert.True(result.IsValid);
        mocker.GetMock<ICustomerRepository>().Verify(r => r.Add(customer), Times.Once);
    }

    [Fact]
    public async Task Given_InvalidCustomer_ShouldNot_Register_Customer()
    {
        var customerViewModel = CustomerFixture.CreateInvalidCustomerViewModel();
        var customer = new Customer(customerViewModel.FirstName,
            customerViewModel.LastName,
            customerViewModel.Email,
            customerViewModel.BirthDate);

        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<CustomerAppService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
            .Returns(customer);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        var result = await service.Register(customerViewModel);

        Assert.False(result.IsValid);
        mocker.GetMock<ICustomerRepository>().Verify(r => r.Add(customer), Times.Never);
    }

    [Fact]
    public async Task Given_CustomerEmail_AlreadyTaken_ShouldNot_Register_Customer()
    {
        var customerViewModel = CustomerFixture.CreateCustomerViewModel();
        var customer = new Customer(customerViewModel.FirstName,
            customerViewModel.LastName,
            customerViewModel.Email,
            customerViewModel.BirthDate);

        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<CustomerAppService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
            .Returns(customer);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.GetByEmail(customerViewModel.Email)).Returns(Task.FromResult(
                new Customer(customerViewModel.FirstName,
                             customerViewModel.LastName,
                             customerViewModel.Email,
                             customerViewModel.BirthDate)));

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        var result = await service.Register(customerViewModel);

        Assert.False(result.IsValid);
        mocker.GetMock<ICustomerRepository>().Verify(r => r.Add(customer), Times.Never);
    }

    [Fact]
    public async Task Given_ValidCustomer_Should_Update_Customer()
    {
        var customerViewModel = CustomerFixture.CreateCustomerViewModel();
        var customer = new Customer(customerViewModel.FirstName,
            customerViewModel.LastName,
            customerViewModel.Email,
            customerViewModel.BirthDate);

        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<CustomerAppService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
            .Returns(customer);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        var result = await service.Update(customerViewModel);

        Assert.True(result.IsValid);
        mocker.GetMock<ICustomerRepository>().Verify(r => r.Update(customer), Times.Once);
    }

    [Fact]
    public async Task Given_ValidCustomerId_Should_Remove_Customer()
    {
        var customer = CustomerFixture.CreateCustomer();

        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<CustomerAppService>();

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.GetById(customer.Id)).Returns(Task.FromResult(customer));

        var result = await service.Remove(customer.Id);

        Assert.True(result.IsValid);
        mocker.GetMock<ICustomerRepository>().Verify(r => r.GetById(customer.Id), Times.Once);
        mocker.GetMock<ICustomerRepository>().Verify(r => r.Remove(customer), Times.Once);
    }

    [Fact]
    public async Task Should_Return_Customers()
    {
        var customerViewModels = await CustomerFixture.CreateCustomerViewModels();
        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<CustomerAppService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<IEnumerable<CustomerViewModel>>(It.IsAny<IEnumerable<Customer>>()))
            .Returns(customerViewModels);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.GetAll()).Returns(CustomerFixture.CreateCustomers);

        var result = await service.GetAll();

        var customerViewModelsList = result.ToList();

        customerViewModelsList.Should()
            .NotBeNull();

        customerViewModelsList.Should()
            .HaveCountGreaterOrEqualTo(0);

        mocker.GetMock<ICustomerRepository>().Verify(r => r.GetAll(), Times.Once);
    }

    [Fact]
    public async Task Given_ValidCustomerId_Should_Return_Customer()
    {
        var customerId = Guid.NewGuid();
        var customerViewModel = CustomerFixture.CreateCustomerViewModel();
        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<CustomerAppService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<CustomerViewModel>(It.IsAny<Customer>()))
            .Returns(customerViewModel);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.GetById(customerId)).Returns(Task.FromResult(CustomerFixture.CreateCustomer()));

        var result = await service.GetById(customerId);

        result.Should().NotBeNull();

        mocker.GetMock<ICustomerRepository>().Verify(r => r.GetById(customerId), Times.Once);
    }
}