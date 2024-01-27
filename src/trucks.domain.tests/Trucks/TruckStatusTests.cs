using FluentAssertions;
using Trucks.domain.Trucks;

namespace Trucks.domain.unittests.Trucks
{
    public class TruckStatusTests
    {
        [Fact]
        public void StartLoading_ShouldSuccess_WhenOutOfService()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");

            //act
            var result = truck.StartLoadingTruck();

            //assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Id.Should().Be(TruckStatus.LoadingStatus.Id);
        }

        [Fact]
        public void StartLoading_ShouldFailed_WhenAtJobStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();
            truck.NotifyAtJob();

            //act

            var result = truck.StartLoadingTruck();

            //assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void StartLoading_ShouldFailed_WheneToJobStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();

            //act
            var result = truck.StartLoadingTruck();

            //assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void StartLoading_ShouldSuccess_WheneReturningStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();
            truck.NotifyAtJob();
            truck.Return();

            //act
            var result = truck.StartLoadingTruck();

            //assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Id.Should().Be(TruckStatus.LoadingStatus.Id);
        }

        [Fact]
        public void SendToJob_ShouldSuccess_WheneLoadingStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();

            //act
            var result = truck.SendToJob();

            //assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Id.Should().Be(TruckStatus.ToJobStatus.Id);
        }

        [Fact]
        public void SendToJob_ShouldFailed_WheneAtJobStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();
            truck.NotifyAtJob();

            //act
            var result = truck.SendToJob();

            //assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void SendToJob_ShouldFailed_WheneReturningStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();
            truck.NotifyAtJob();
            truck.Return();

            //act
            var result = truck.SendToJob();

            //assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void NotifyAtJob_ShouldSuccess_WheneToJobStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();

            //act
            var result = truck.NotifyAtJob();

            //assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Id.Should().Be(TruckStatus.AtJobStatus.Id);
        }

        [Fact]
        public void NotifyAtJob_ShouldFailed_WheneLoadingStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();

            //act
            var result = truck.NotifyAtJob();

            //assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void NotifyAtJob_ShouldFailed_WheneReturningStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();
            truck.NotifyAtJob();
            truck.Return();

            //act
            var result = truck.NotifyAtJob();

            //assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void Return_ShouldSuccess_WhenAtJobStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();
            truck.NotifyAtJob();

            //act
            var result = truck.Return();

            //assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Id.Should().Be(TruckStatus.ReturningStatus.Id);
        }

        [Fact]
        public void Return_ShouldFailed_WheneLoadingStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();

            //act
            var result = truck.Return();

            //assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void Return_ShouldFailed_WheneToJobStatus()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();
            truck.SendToJob();

            //act
            var result = truck.Return();

            //assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void ChangeToOutOfService_ShouldSuccess_WhenLoading()
        {
            //prepare
            var truck = CreateTruckWithOutOfServiceStatus("ABC999");
            truck.StartLoadingTruck();

            //act
            var result = truck.ChangeToOutOfService();

            //assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Id.Should().Be(TruckStatus.OutOfServiceStatus.Id);
        }

        private Truck CreateTruckWithOutOfServiceStatus(string code) => Truck.Create(Guid.NewGuid(), code, "TestTruck", string.Empty).Value;
    }
}
