using CancunHotel.Test.Fixtures;
using Xunit;

namespace CancunHotel.Test.Entities;

public class EntityTest : IClassFixture<EntityFixture>
{
    private readonly EntityFixture _fixture;

    public EntityTest(EntityFixture fixture)
    {
        _fixture = fixture;
    }
}