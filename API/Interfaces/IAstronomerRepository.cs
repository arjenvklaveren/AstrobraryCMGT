using System;
using API.Entities;
using API.Helpers.Types;

namespace API.Interfaces;

public interface IAstronomerRepository
{
    Task<IEnumerable<Astronomer>> GetAllAsync(AstronomerFilterParams filterParams);
    Task<Astronomer?> GetByIdAsync(int id);
    Task<Astronomer> AddAsync(Astronomer astronomer);
    Task<Astronomer> UpdateAsync(Astronomer astronomer);
    Task RemoveAsync(int id);
}
