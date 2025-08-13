using System;
using API.Entities;

namespace API.Interfaces;

public interface IAstronomerRepository
{
    Task<IEnumerable<Astronomer>> GetAllAsync();
}
