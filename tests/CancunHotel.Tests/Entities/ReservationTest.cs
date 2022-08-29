using System;
using CancunHotel.Domain.Entities;
using CancunHotel.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace CancunHotel.Tests.Entities;

public class ReservationTest
{
    [Fact]
    public void Should_Create_Valid_Reservation()
    {
        var reservation = ReservationFixture.CreateValidReservation();

        reservation.CheckInDate
            .Should()
            .Be(reservation.CheckInDate);
        
        reservation.CheckOutDate
            .Should()
            .Be(reservation.CheckOutDate);

        reservation.GuestsAmount
            .Should()
            .BeGreaterThanOrEqualTo(1);
        
        reservation.Customer
            .Should()
            .NotBeNull();
    }
    
    [Fact]
    public void Should_Create_Valid_Reservation_With_Custom_Customer()
    {
        var customer = CustomerFixture.CreateCustomer();
        
        var reservation = new Reservation(customer)
        {
            CheckInDate = DateTimeOffset.UtcNow,
            CheckOutDate = DateTimeOffset.UtcNow.AddDays(3),
            GuestsAmount = 4
        };

        reservation.CheckInDate
            .Should()
            .Be(reservation.CheckInDate);
        
        reservation.CheckOutDate
            .Should()
            .Be(reservation.CheckOutDate);

        reservation.GuestsAmount
            .Should()
            .BeGreaterThan(1);

        reservation.CustomerId
            .Should()
            .NotBeEmpty();
        
        reservation.Customer
            .Should()
            .NotBeNull();
    }
}