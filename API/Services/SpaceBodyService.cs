using System;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class SpaceBodyService(ISpaceBodyRepository spaceBodyRepository) : ISpaceBodyService
{
    public async Task<IEnumerable<SpaceBody>> GetAllAsync()
    {
        return await spaceBodyRepository.GetAllAsync();
    }
}
