using System;
using CancunHotel.Domain.DomainObjects;
using CancunHotel.Domain.Interfaces;

namespace CancunHotel.Test.Fixtures;

public class EntityFixture
{
    public static TestEntity CreateEntity() => new();

    public static void ModifyUpdateAt(IEntity entity) => entity.UpdatedAt = DateTimeOffset.UtcNow;

    public class TestEntity : Entity
    {
    }
}