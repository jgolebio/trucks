using FluentResults;
using Trucks.domain.Trucks;

namespace trucks.application.Services;
public class TruckUniqueCodeService : ITruckUniqueCodeService
{
    private readonly ITrucksRepository _trucksRepository;

    public TruckUniqueCodeService(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }

    public async Task<Result<bool>> IsUniqueAsync(string code)
    {
        var result = await _trucksRepository.IsAnyWithCodeAsync(code);
        if (result.IsFailed)
            return result.ToResult();

        return Result.Ok(!result.Value);
    }
}
