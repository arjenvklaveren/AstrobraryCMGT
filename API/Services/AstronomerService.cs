using System;
using API.Entities;
using API.Helpers.Types;
using API.Interfaces;

namespace API.Services;

public class AstronomerService(IAstronomerRepository astronomerRepository) : IAstronomerService
{
    public async Task<IReadOnlyList<Astronomer>> GetAllAstronomersAsync(AstronomerFilterParams filterParams)
    {
        var astronomers = await astronomerRepository.GetAllAsync(filterParams);
        return astronomers.ToList();
    }

    public async Task<Astronomer?> GetAstronomerByIdAsync(int id)
    {
        return await astronomerRepository.GetByIdAsync(id);
    }

    public async Task<int?> AddAstronomerAsync(Astronomer astronomer)
    {
        astronomer = await astronomerRepository.AddAsync(astronomer);
        return astronomer.Id;
    }

    public async Task<Astronomer> UpdateAstronomerAsync(Astronomer astronomer)
    {
        return await astronomerRepository.UpdateAsync(astronomer);
    }

    public async Task RemoveAstronomerAsync(int id)
    {
        await astronomerRepository.RemoveAsync(id);
    }
}
