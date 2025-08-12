using System;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class SpaceBodyRepository(DatabaseContext context) : ISpaceBodyRepository
{
    public async Task<IEnumerable<SpaceBody>> GetAllAsync()
    {
        return await context.SpaceBodies.ToListAsync();
    }
}
