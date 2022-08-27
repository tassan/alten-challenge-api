using System;
using Bogus;
using CancunHotel.Domain.Entities;

namespace CancunHotel.Test.Fixtures;

public static class ReservationFixture
{
    public static Reservation CreateValidReservation()
    {
        var checkInDate = new Faker().Date.SoonOffset(1, DateTimeOffset.UtcNow);
        var checkOutDate = new Faker().Date.SoonOffset(30, checkInDate);
        
        return new Faker<Reservation>()
            .RuleFor(r => r.CheckInDate, checkInDate)
            .RuleFor(r => r.CheckOutDate, checkOutDate)
            .RuleFor(r => r.GuestsAmount, f => f.Random.Int(1, 4))
            .RuleFor(r => r.Customer, CustomerFixture.CreateCustomer);
    }
}