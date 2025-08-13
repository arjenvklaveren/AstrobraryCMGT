using System;
using API.Entities;

namespace API.Interfaces;

public interface IAstronomerService
{
    Task<IReadOnlyList<Astronomer>> GetAllAstronomersAsync();
}
