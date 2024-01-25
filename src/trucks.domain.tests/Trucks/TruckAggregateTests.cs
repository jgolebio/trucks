using FluentAssertions;
using trucks.domain.Trucks;

namespace trucks.domain.unittests.Trucks;

public class TruckAggregateTests
{
    [Fact]
    public void Create_ShouldCreate_Properly()
    {
        //act
        var result = Truck.Create("AAA999", "testOne", "");

        //assert
        result.IsSuccess.Should().BeTrue();
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
}
