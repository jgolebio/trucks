using FluentResults;
using trucks.domain.SeedWork;

namespace trucks.domain.Trucks;

public class Truck : Entity, IAggregateRoot
{
    public TruckCode Code { get; }
    public TruckName Name { get; }
    public TruckDescription Description { get; }
    public TruckStatus Status { get; private set; }

    internal Truck(Guid id, TruckCode code, TruckName name, TruckDescription description, TruckStatus status) : base(id)
    {
        Code = code;
        Name = name;
        Description = description;
        Status = status;
    }

    public static Result<Truck> Create(string code, string name, string description)
    {
        var codeRes = TruckCode.Create(code);
        var nameRes = TruckName.Create(name);
        var descriptionRes = TruckDescription.Create(description);
        var mergedRes = Result.Merge(codeRes, nameRes, descriptionRes);
        if (mergedRes.IsFailed)
            return mergedRes;

        var instance = new Truck(Guid.NewGuid(), codeRes.Value, nameRes.Value, descriptionRes.Value, TruckStatus.OutOfServiceStatus);

        return Result.Ok(instance);
    }

    public Result<Truck> StartLoadingTruck()
    {
        var result = Status.MoveToLoadingStatus();
        if (result.IsFailed)
            return result.ToResult();

        Status = result.Value;

        return Result.Ok(this);
    }

    public Result<Truck> SendToJob()
    {
        var result = Status.MoveToJobStatus();
        if (result.IsFailed)
            return result.ToResult();

        Status = result.Value;

        return Result.Ok(this);
    }

    public Result<Truck> NotifyAtJob()
    {
        var result = Status.MoveToAtJobStatus();
        if (result.IsFailed)
            return result.ToResult();

        Status = result.Value;

        return Result.Ok(this);
    }

    public Result<Truck> Return()
    {
        var result = Status.MoveToReturningStatus();
        if (result.IsFailed)
            return result.ToResult();

        Status = result.Value;

        return Result.Ok(this);
    }

    public Result<Truck> ChangeToOutOfService()
    {
        var result = Status.MoveToOutOfService();
        if (result.IsFailed)
            return result.ToResult();

        Status = result.Value;

        return Result.Ok(this);
    }
}
