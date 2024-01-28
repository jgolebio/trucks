using FluentResults;

namespace trucks.application.Services;
public interface ITruckUniqueCodeService
{
    Task<Result<bool>> IsUniqueAsync(string code);
}