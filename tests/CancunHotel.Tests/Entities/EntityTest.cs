using System;
using CancunHotel.Domain.DomainObjects;
using CancunHotel.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace CancunHotel.Tests.Entities;

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

        entity.CreatedAt
            .Should()
            .BeSameDateAs(DateTime.UtcNow);
        
        entity.UpdatedAt
            .Should()
            .BeSameDateAs(DateTime.UtcNow);
    }

    [Fact]
    public void Should_Modify_UpdateAt_Property()
    {
        var entity = EntityFixture.CreateEntity();
        
        EntityFixture.ModifyUpdateAt(entity);
        
        entity.UpdatedAt
            .Should()
            .BeSameDateAs(DateTime.UtcNow);
    }

    [Fact]
    public void Should_Be_False_When_Distinct_Instance()
    {
        var aEntity = EntityFixture.CreateEntity();
        var bEntity = EntityFixture.CreateEntity();

        aEntity.Equals(bEntity)
            .Should()
            .BeFalse();
    }
    
    [Fact]
    public void Should_Be_False_When_Null()
    {
        var aEntity = EntityFixture.CreateEntity();

        aEntity.Equals(null)
            .Should()
            .BeFalse();
    }
    
    [Fact]
    public void Should_Be_True_When_Same_Instance()
    {
        var aEntity = EntityFixture.CreateEntity();
        var bEntity = aEntity;

        aEntity.Equals(bEntity)
            .Should()
            .BeTrue();
    }
    
    [Fact]
    public void Test_Entity_Equality_Operator_With_Instance()
    {
        var aEntity = EntityFixture.CreateEntity();
        var bEntity = EntityFixture.CreateEntity();
        
        (aEntity == bEntity)
            .Should()
            .BeFalse();
    }
    
    [Fact]
    public void Test_Entity_Equality_Operator_With_Null()
    {
        var aEntity = EntityFixture.CreateEntity();
        Entity bEntity = null;
        
        (aEntity == null)
            .Should()
            .BeFalse();

        (bEntity == bEntity)
            .Should()
            .BeTrue();
    }
    
    [Fact]
    public void Test_Entity_Inequality_Operator()
    {
        var aEntity = EntityFixture.CreateEntity();
        var bEntity = EntityFixture.CreateEntity();
        
        (aEntity != bEntity)
            .Should()
            .BeTrue();
    }
    
    [Fact]
    public void Test_Entity_GetHashCode()
    {
        var aEntity = EntityFixture.CreateEntity();
        var bEntity = EntityFixture.CreateEntity();

        aEntity.GetHashCode()
            .Should()
            .NotBe(bEntity.GetHashCode());
    }
    
    [Fact]
    public void Test_Entity_ToString()
    {
        var aEntity = EntityFixture.CreateEntity();

        aEntity.ToString()
            .Should()
            .Contain(aEntity.Id.ToString());
        
        aEntity.ToString()
            .Should()
            .Contain(aEntity.GetType().Name);
    }
}