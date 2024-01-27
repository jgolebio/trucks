using FluentAssertions;
using trucks.domain.Events;
using Trucks.domain.Trucks;

namespace Trucks.domain.unittests.Trucks;

public class TruckAggregateTests
{
    [Fact]
    public void Create_ShouldCreate_Properly()
    {
        //act
        var result = Truck.Create(Guid.NewGuid(), "AAA999", "testOne", "");

        //assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Create_ShouldAddCreateDomainEvent()
    {
        //act
        var result = Truck.Create(Guid.NewGuid(), "AAA999", "testOne", "");

        //assertion
        result.Value.DomainEvents.Should().HaveCount(1);
        var @event = result.Value.DomainEvents.First() as TruckCreatedDomainEvent;
        @event.Should().NotBeNull();
        @event.Id.Should().Be(result.Value.Id.Value);
        @event.Code.Should().Be(result.Value.Code.Value);
        @event.CreateDate.Should().NotBe(DateTime.MinValue);

    }

    [Theory]
    [InlineData("------")]
    [InlineData("AAA-99999")]
    [InlineData("00@@@!!!!!")]
    public void CreateTruckCode_ShouldFail_WhenWrongFormat(string code)
    {
        //act 
        var result = TruckCode.Create(code);

        //assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void CreateTruckCode_ShouldFail_WhenCodeEmpty()
    {
        //act
        var result = TruckCode.Create(string.Empty);

        //assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void CreateTruckCode_ShouldCreateProperly()
    {
        //prepare
        string code = "ADDD9999";

        //act
        var result = TruckCode.Create(code);

        //assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(code);
    }

    [Fact]
    public void CreateTruckName_ShouldFail_WhenEmpty()
    {
        //act
        var result = TruckName.Create(string.Empty);

        //assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Update_ShouldSuccess()
    {
        //prepare 
        const string name = "New one";
        const string code = "AAA999";
        const string description = "truck info";
        var truck = Truck.Create(Guid.NewGuid(), code, name, description).Value;

        const string newName = "Old one";

        //act
        var updateRes = truck.Update(code, newName, description);

        //assertion
        updateRes.IsSuccess.Should().BeTrue();
        updateRes.Value.Name.Value.Should().Be(newName);
    }
}
