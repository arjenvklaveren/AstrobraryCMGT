using System;
using API.DTO_s;
using API.Entities;
using API.Helpers.Types;

namespace API.Interfaces;

public interface ISpaceBodyService
{
    Task<IReadOnlyList<SpaceBodyDTO>> GetAllBodiesAsync(SpaceBodyFilterParams filterParams);
    Task<SpaceBodyDTO?> GetBodyByIdAsync(int id);
    Task<SpaceBodyDTO?> GetRootSpaceBodyByIdAsync(int id);
    Task<SpaceBodyDTO?> GetSpaceBodyHierarchyAsync(SpaceBodyDTO spaceBodyDTO);
    Task<IReadOnlyList<SpaceBodyDTO>> GetAllBodiesOfAstronomer(int astronomerId);
}
