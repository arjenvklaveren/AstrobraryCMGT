using System;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class SpaceBodyService(ISpaceBodyRepository spaceBodyRepository) : ISpaceBodyService
{
    public async Task<IReadOnlyList<SpaceBody>> GetAllBodiesAsync()
    {
        var bodies = await spaceBodyRepository.GetAllAsync();
        return bodies.ToList();
    }
}
