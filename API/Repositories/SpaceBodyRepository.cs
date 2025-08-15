using System;
using API.Data;
using API.Entities;
using API.Helpers.Types;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class SpaceBodyRepository(DatabaseContext context) : ISpaceBodyRepository
{
    public async Task<IEnumerable<SpaceBody>> GetAllAsync(SpaceBodyFilterParams filterParams)
    {
        var query = context.SpaceBodies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterParams.Name))
        {
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{filterParams.Name}%"));
        }

        if (filterParams.Age.HasValue)
            {
                query = query.Where(x => x.Age > filterParams.Age.Value);
            }
            
        if (filterParams.HasRings.HasValue)
        {
            query = filterParams.HasRings.Value
                ? query.Where(x => x.RingSystem != null)
                : query.Where(x => x.RingSystem == null);
        }

        if (filterParams.BodyType.HasValue)
        {
            query = query.Where(x => x.Type == filterParams.BodyType);
        }

        return await query.ToListAsync();
    }

    public async Task<SpaceBody?> GetByIdAsync(int id)
    {
        return await context.SpaceBodies
            .Include(x => x.RingSystem)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<SpaceBody>> GetAllChildrenAsync(int id)
    {
        return await context.SpaceBodies
            .Include(x => x.RingSystem)
            .Where(x => x.ParentId == id)
            .ToListAsync();
    }
}
