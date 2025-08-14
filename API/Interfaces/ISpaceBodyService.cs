using System;
using API.DTO_s;
using API.Entities;

namespace API.Interfaces;

public interface ISpaceBodyService
{
    Task<IReadOnlyList<SpaceBodyDTO>> GetAllBodiesAsync();
    Task<SpaceBodyDTO?> GetBodyByIdAsync(int id);
    Task<SpaceBodyDTO?> GetRootSpaceBodyByIdAsync(int id);
    Task<SpaceBodyDTO?> GetSpaceBodyHierarchyAsync(SpaceBodyDTO spaceBodyDTO);
}
