using System;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AstronomerRepository(DatabaseContext context) : IAstronomerRepository
{
    public async Task<IEnumerable<Astronomer>> GetAllAsync()
    {
        return await context.Astronomers.ToListAsync();
    }

    public async Task<Astronomer?> GetByIdAsync(int id)
    {
        return await context.Astronomers.FindAsync(id);
    }
}
