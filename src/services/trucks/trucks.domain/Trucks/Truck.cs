﻿using FluentResults;
using Trucks.domain.SeedWork;
using Trucks.domain.Trucks.DbSnapshots;

namespace Trucks.domain.Trucks;

public class Truck : Entity, IAggregateRoot
{
    public TruckCode Code { get; private set; }
    public TruckName Name { get; private set; }
    public TruckDescription Description { get; private set; }
    public TruckStatus Status { get; private set; }

    internal Truck(TruckId id, TruckCode code, TruckName name, TruckDescription description, TruckStatus status) : base(id)
    {
        Code = code;
        Name = name;
        Description = description;
        Status = status;
    }

    public static Result<Truck> Create(Guid id, string code, string name, string description, TruckStatus status)
    {
        var truckIdRes = TruckId.Create(id);
        var codeRes = TruckCode.Create(code);
        var nameRes = TruckName.Create(name);
        var descriptionRes = TruckDescription.Create(description);
        var mergedRes = Result.Merge(codeRes, nameRes, descriptionRes, truckIdRes);
        if (mergedRes.IsFailed)
            return mergedRes;

        var instance = new Truck(truckIdRes.Value, codeRes.Value, nameRes.Value, descriptionRes.Value, status);

        return Result.Ok(instance);
    }

    public static Result<Truck> Create(Guid id, string code, string name, string description) =>
        Create(id, code, name, description, TruckStatus.OutOfServiceStatus);

    public Result<Truck> Update(string code, string name, string description)
    {
        var codeRes = TruckCode.Create(code);
        var nameRes = TruckName.Create(name);
        var descriptionRes = TruckDescription.Create(description);

        var mergedRes = Result.Merge(codeRes, nameRes, descriptionRes);
        if (mergedRes.IsFailed)
            return mergedRes;

        Code = codeRes.Value;
        Name = nameRes.Value;
        Description = descriptionRes.Value;

        return Result.Ok(this);
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

    public TruckDbSnapshot ToDbSnapshot() =>
        new TruckDbSnapshot(Id.Value, Code.Value, Name.Value, Description.Value, Status.Id);

    public static Result<Truck> CreateFromDbSnapshot(TruckDbSnapshot dbSnapshot) =>
        Truck.Create(dbSnapshot.Id, dbSnapshot.Code, dbSnapshot.Name, dbSnapshot.Description, Enumeration.FromValue<TruckStatus>(dbSnapshot.Status));
}
