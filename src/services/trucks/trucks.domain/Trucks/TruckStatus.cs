using FluentResults;
using trucks.domain.SeedWork;

namespace trucks.domain.Trucks;

public class TruckStatus : Enumeration
{
    public static readonly OutOfServiceTruckStatus OutOfServiceStatus = new();
    public static readonly LoadingTruckStatus LoadingStatus = new();
    public static readonly ToJobTruckStatus ToJobStatus = new();
    public static readonly AtJobTruckStatus AtJobStatus = new();
    public static readonly ReturningTruckStatus ReturningStatus = new();

    protected TruckStatus(int id, string name) : base(id, name)
    {
    }

    public virtual Result<TruckStatus> MoveToOutOfService() => Result.Ok<TruckStatus>(OutOfServiceStatus);
    public virtual Result<TruckStatus> MoveToLoadingStatus() => Result.Fail("Operation is not allowed");
    public virtual Result<TruckStatus> MoveToJobStatus() => Result.Fail("Operation is not allowed");
    public virtual Result<TruckStatus> MoveToAtJobStatus() => Result.Fail("Operation is not allowed");
    public virtual Result<TruckStatus> MoveToReturningStatus() => Result.Fail("Operation is not allowed");
}

public class OutOfServiceTruckStatus : TruckStatus
{
    internal OutOfServiceTruckStatus() : base(1, "Out of service") { }

    public override Result<TruckStatus> MoveToLoadingStatus() =>
        Result.Ok<TruckStatus>(LoadingStatus);

    public override Result<TruckStatus> MoveToAtJobStatus() =>
        Result.Ok<TruckStatus>(AtJobStatus);

    public override Result<TruckStatus> MoveToJobStatus() =>
        Result.Ok<TruckStatus>(ToJobStatus);

    public override Result<TruckStatus> MoveToReturningStatus() =>
        Result.Ok<TruckStatus>(ReturningStatus);

    public override Result<TruckStatus> MoveToOutOfService() =>
        Result.Fail("There is already status \"Out of service\"");
}

public class LoadingTruckStatus : TruckStatus
{
    internal LoadingTruckStatus() : base(2, "Loading") { }

    public override Result<TruckStatus> MoveToJobStatus() =>
        Result.Ok<TruckStatus>(ToJobStatus);
}

public class ToJobTruckStatus : TruckStatus
{
    internal ToJobTruckStatus() : base(3, "To Job") { }

    public override Result<TruckStatus> MoveToAtJobStatus() =>
        Result.Ok<TruckStatus>(AtJobStatus);
}

public class AtJobTruckStatus : TruckStatus
{
    internal AtJobTruckStatus() : base(4, "At Job") { }

    public override Result<TruckStatus> MoveToReturningStatus() =>
        Result.Ok<TruckStatus>(ReturningStatus);
}

public class ReturningTruckStatus : TruckStatus
{
    internal ReturningTruckStatus() : base(5, "Returning") { }

    public override Result<TruckStatus> MoveToLoadingStatus() =>
        Result.Ok<TruckStatus>(LoadingStatus);
}
