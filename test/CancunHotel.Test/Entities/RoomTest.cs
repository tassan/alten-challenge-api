using CancunHotel.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace CancunHotel.Test.Entities;

public class RoomTest : IClassFixture<RoomFixture>
{
    private readonly RoomFixture _fixture;

    public RoomTest(RoomFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Should_Create_Valid_Room()
    {
        var room = RoomFixture.CreateRoom();

        room.BedsAmount
            .Should()
            .BeGreaterThanOrEqualTo(1);
    }
}