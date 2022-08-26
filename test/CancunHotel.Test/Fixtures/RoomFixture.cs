using System;
using Bogus;
using CancunHotel.Domain.Entities;

namespace CancunHotel.Test.Fixtures;

public class RoomFixture : IDisposable
{
    public static Room CreateRoom()
    {
        return new Faker<Room>()
            .RuleFor(r => r.BedsAmount, f => f.Random.Int(1, 4))
            .Generate();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}