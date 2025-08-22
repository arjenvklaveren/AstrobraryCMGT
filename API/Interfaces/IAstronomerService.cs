using System;
using API.Entities;
using API.Helpers.Types;

namespace API.Interfaces;

public interface IAstronomerService
{
    Task<IReadOnlyList<Astronomer>> GetAllAstronomersAsync(AstronomerFilterParams filterParams);
    Task<Astronomer?> GetAstronomerByIdAsync(int id);
    Task<int?> AddAstronomerAsync(Astronomer astronomer);
    Task<Astronomer> UpdateAstronomerAsync(Astronomer astronomer);
    Task RemoveAstronomerAsync(int id);
    Task<string> SetAstronomerImage(IFormFile file, int id);
}
