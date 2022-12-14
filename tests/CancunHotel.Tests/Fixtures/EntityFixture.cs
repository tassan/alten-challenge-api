using System;
using CancunHotel.Domain.DomainObjects;
using CancunHotel.Domain.Interfaces;

namespace CancunHotel.Tests.Fixtures;

public class EntityFixture
{
    public static TestEntity CreateEntity() => new();

    public static void ModifyUpdateAt(IEntity entity) => entity.UpdatedAt = DateTime.UtcNow;

    public class TestEntity : Entity
    {
    }
}