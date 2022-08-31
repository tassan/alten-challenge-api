using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using CancunHotel.Application.Services;
using CancunHotel.Application.ViewModels;
using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Tests.Fixtures;
using Moq;
using Moq.AutoMock;
using Xunit;
using Xunit.Abstractions;

namespace CancunHotel.Tests.Services;

public class BookingServiceTests
{
    private readonly ITestOutputHelper _outputHelper;

    public BookingServiceTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task Given_ValidBooking_Should_Register_Reservation()
    {
        var customer = CustomerFixture.CreateCustomer();
        var checkInDate = new Faker().Date.Soon(1, DateTime.UtcNow);
        var checkOutDate = checkInDate.AddDays(3);
        var guests = new Faker().Random.Int(1, 10);
        var reservation = new Reservation(customer.Id,
            checkInDate,
            checkOutDate,
            guests)
        {
            Customer = customer,
            CustomerId = customer.Id
        };

        var createBookingVm = new CreateBookingViewModel
        {
            CustomerId = reservation.CustomerId,
            GuestsAmount = reservation.GuestsAmount,
            CheckInDate = reservation.CheckInDate,
            CheckOutDate = reservation.CheckOutDate
        };

        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<BookingService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<Reservation>(It.IsAny<CreateBookingViewModel>()))
            .Returns(reservation);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.GetById(customer.Id)).Returns(Task.FromResult(customer));

        mocker.GetMock<IReservationRepository>()
            .Setup(r => r.GetByDates(reservation.CheckInDate, reservation.CheckOutDate))
            .Returns(() => new List<Reservation>().AsQueryable());
        
        mocker.GetMock<IReservationRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        var result = await service.Register(createBookingVm);
        _outputHelper.WriteLine(string.Join(", ", result.Errors));
        Assert.True(result.IsValid);
        mocker.GetMock<IReservationRepository>().Verify(r => r.Add(reservation), Times.Once);
    }

    [Fact]
    public async Task Given_InvalidBooking_ShouldNot_Register_Reservation()
    {
        var customer = CustomerFixture.CreateCustomer();
        var checkInDate = new Faker().Date.Soon(1, DateTime.UtcNow);
        var checkOutDate = checkInDate.AddDays(5);
        var guests = new Faker().Random.Int(1, 10);
        var reservation = new Reservation(customer.Id,
            checkInDate,
            checkOutDate,
            guests)
        {
            Customer = customer,
            CustomerId = customer.Id
        };

        var createBookingVm = new CreateBookingViewModel
        {
            CustomerId = reservation.CustomerId,
            GuestsAmount = reservation.GuestsAmount,
            CheckInDate = reservation.CheckInDate,
            CheckOutDate = reservation.CheckOutDate
        };

        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<BookingService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<Reservation>(It.IsAny<CreateBookingViewModel>()))
            .Returns(reservation);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.GetById(customer.Id)).Returns(Task.FromResult(customer));

        mocker.GetMock<IReservationRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        var result = await service.Register(createBookingVm);
        _outputHelper.WriteLine(string.Join(", ", result.Errors));
        Assert.False(result.IsValid);
        mocker.GetMock<IReservationRepository>().Verify(r => r.Add(reservation), Times.Never);
    }

    [Theory, MemberData(nameof(IncorrectCheckInDates))]
    public async Task Given_CheckIn_And_CheckOut_Dates_AlreadyTaken_ShouldNot_Register_Reservation(DateTime checkInDate, DateTime checkOutDate)
    {
        var customer = CustomerFixture.CreateCustomer();
        var guests = new Faker().Random.Int(1, 10);
        var reservation = new Reservation(customer.Id,
            checkInDate,
            checkOutDate,
            guests)
        {
            Customer = customer,
            CustomerId = customer.Id
        };

        var createBookingVm = new CreateBookingViewModel
        {
            CustomerId = reservation.CustomerId,
            GuestsAmount = reservation.GuestsAmount,
            CheckInDate = reservation.CheckInDate,
            CheckOutDate = reservation.CheckOutDate
        };

        var readBookingVm = new ReadBookingViewModel
        {
            Id = reservation.Id,
            CustomerId = reservation.CustomerId,
            GuestsAmount = reservation.GuestsAmount,
            CheckInDate = reservation.CheckInDate,
            CheckOutDate = reservation.CheckOutDate,
            Customer = new CustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                BirthDate = customer.BirthDate
            }
        };

        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<BookingService>();

        mocker.GetMock<IMapper>().Setup(x => x.Map<Reservation>(It.IsAny<CreateBookingViewModel>()))
            .Returns(reservation);

        mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.GetById(customer.Id)).Returns(Task.FromResult(customer));

        mocker.GetMock<IReservationRepository>()
            .Setup(r => r.GetByDates(reservation.CheckInDate, reservation.CheckOutDate))
            .Returns(() => new List<Reservation> {reservation}.AsQueryable());
        
        mocker.GetMock<IReservationRepository>()
            .Setup(r => r.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

        var result = await service.Register(createBookingVm);
        _outputHelper.WriteLine(string.Join(", ", result.Errors));
        Assert.False(result.IsValid);
        mocker.GetMock<IReservationRepository>().Verify(r => r.Add(reservation), Times.Never);
    }

    public static readonly object[][] IncorrectCheckInDates = {
        new object[] { DateTime.UtcNow, DateTime.UtcNow.AddDays(0) },
        new object[] { DateTime.UtcNow, DateTime.UtcNow.AddDays(-1) },
        new object[] { DateTime.UtcNow, DateTime.UtcNow.AddDays(10) },
        new object[] { DateTime.UtcNow.AddDays(60), DateTime.UtcNow.AddDays(3) },
    };
}