using System;
using API.Data;
using API.Entities;
using API.Helpers.Types;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AstronomerRepository(DatabaseContext context) : IAstronomerRepository
{
    public async Task<IEnumerable<Astronomer>> GetAllAsync(AstronomerFilterParams filterParams)
    {
        var query = context.Astronomers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterParams.Name))
        {
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{filterParams.Name}%"));
        }

        if (filterParams.Age.HasValue)
        {
            query = query.Where(x => filterParams.Age <= (DateTime.Today.Year - x.DateOfBirth.Year));
        }

        if (filterParams.IsMarried.HasValue)
        {
            query = filterParams.IsMarried.Value
                ? query.Where(x => x.Married == true)
                : query.Where(x => x.Married == false);
        }

        if (filterParams.Occupation.HasValue)
        {
            query = query.Where(x => x.Occupation == filterParams.Occupation);
        }

        return await query.ToListAsync();
    }

    public async Task<Astronomer?> GetByIdAsync(int id)
    {
        return await context.Astronomers.FindAsync(id);
    }

    public async Task<Astronomer> AddAsync(Astronomer astronomer)
    {
        context.Astronomers.Add(astronomer);
        await context.SaveChangesAsync();
        return astronomer;
    }

    public async Task<Astronomer> UpdateAsync(Astronomer astronomer)
    {
        //TODO DOES NOT WORK YET
        return astronomer;
    }

    public async Task RemoveAsync(int id)
    {
        var astronomer = await context.Astronomers.FindAsync(id);
        if (astronomer != null)
        {
            context.Astronomers.Remove(astronomer);
            await context.SaveChangesAsync();
        }
    }
}
