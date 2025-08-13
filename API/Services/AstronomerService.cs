using System;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class AstronomerService(IAstronomerRepository astronomerRepository) : IAstronomerService
{
    public async Task<IReadOnlyList<Astronomer>> GetAllAstronomersAsync()
    {
        var astronomers = await astronomerRepository.GetAllAsync();
        return astronomers.ToList();
    }

    public async Task<Astronomer?> GetAstronomerByIdAsync(int id)
    {
        return await astronomerRepository.GetByIdAsync(id);
    }
}
