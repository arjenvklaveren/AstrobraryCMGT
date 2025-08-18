using System;
using API.Entities;
using API.Helpers.Types;

namespace API.Interfaces;

public interface ISpaceBodyRepository
{
    Task<IEnumerable<SpaceBody>> GetAllAsync(SpaceBodyFilterParams filterParams);
    Task<SpaceBody?> GetByIdAsync(int id);
    Task<IEnumerable<SpaceBody>> GetAllChildrenAsync(int? id);
    Task<IReadOnlyList<SpaceBody>> GetAllFromAstronomer(int astronomerId);
    Task RemoveAsync(int id);
}
