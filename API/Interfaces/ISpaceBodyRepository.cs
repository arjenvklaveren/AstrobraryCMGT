using System;
using API.Entities;

namespace API.Interfaces;

public interface ISpaceBodyRepository
{
    Task<IEnumerable<SpaceBody>> GetAllAsync();
    Task<SpaceBody?> GetByIdAsync(int id);
}
