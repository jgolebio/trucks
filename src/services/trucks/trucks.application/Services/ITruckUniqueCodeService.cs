using FluentResults;

namespace Trucks.Application.Services;
public interface ITruckUniqueCodeService
{
    Task<Result<bool>> IsUniqueAsync(string code);
}