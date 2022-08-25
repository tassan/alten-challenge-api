using System;
using CancunHotel.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace CancunHotel.Test.Entities;

public class EntityTest : IClassFixture<EntityFixture>
{
    private readonly EntityFixture _fixture;

    public EntityTest(EntityFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Should_Create_Entity()
    {
        var entity = EntityFixture.CreateEntity();

        entity.Id
            .GetType()
            .Should()
            .Be(typeof(Guid));
        
        entity.Deleted
            .Should()
            .BeFalse();

        entity.CreatedAt
            .Should()
            .BeSameDateAs(DateTimeOffset.UtcNow);
        
        entity.UpdatedAt
            .Should()
            .NotBeSameDateAs(DateTimeOffset.UtcNow);
    }

    [Fact]
    public void Should_Update_UpdateAtProperty()
    {
        var entity = EntityFixture.CreateEntity();
        
        EntityFixture.ModifyUpdateAt(entity);
        
        entity.UpdatedAt
            .Should()
            .BeSameDateAs(DateTimeOffset.UtcNow);
    }
    
    
}