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

    public async Task<SpaceBody?> GetByIdAsync(int id)
    {
        return await context.SpaceBodies.Include(x => x.RingSystem).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<SpaceBody>> GetAllChildrenAsync(int id)
    {
        return await context.SpaceBodies
            .Include(x => x.RingSystem)
            .Where(x => x.ParentId == id)
            .ToListAsync();
    }
}
