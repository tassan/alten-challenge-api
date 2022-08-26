using System;
using CancunHotel.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace CancunHotel.Test.Entities;

public class EntityTest
{
    [Fact]
    public void Should_Create_Valid_Entity()
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
    public void Should_Modify_UpdateAt_Property()
    {
        var entity = EntityFixture.CreateEntity();
        
        EntityFixture.ModifyUpdateAt(entity);
        
        entity.UpdatedAt
            .Should()
            .BeSameDateAs(DateTimeOffset.UtcNow);
    }
}