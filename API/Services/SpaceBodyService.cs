using System;
using API.DTO_s;
using API.Entities;
using API.Helpers.Extensions;
using API.Interfaces;

namespace API.Services;

public class SpaceBodyService(ISpaceBodyRepository spaceBodyRepository) : ISpaceBodyService
{
    public async Task<IReadOnlyList<SpaceBodyDTO>> GetAllBodiesAsync()
    {
        var bodies = await spaceBodyRepository.GetAllAsync();

        return bodies
            .Select(body => body.ToDTO())
            .ToList();
    }

    public async Task<SpaceBodyDTO?> GetBodyByIdAsync(int id)
    {
        var spaceBody = await spaceBodyRepository.GetByIdAsync(id);
        if (spaceBody == null) return null;
        return spaceBody.ToDTO();
    }

    public async Task<SpaceBodyDTO?> GetRootSpaceBodyByIdAsync(int id)
    {
        var currentBody = await GetBodyByIdAsync(id);
        if (currentBody == null) return null;

        var path = new HashSet<int>();
        while (currentBody?.ParentId != null)
        {
            if (path.Contains(currentBody.ParentId.Value))
            {
                currentBody.ParentId = null;
                break;
            }

            path.Add(currentBody.ParentId.Value);
            currentBody = await GetBodyByIdAsync(currentBody.ParentId.Value);
        }

        return currentBody;
    }

    public async Task<SpaceBodyDTO?> GetSpaceBodyHierarchyAsync(SpaceBodyDTO spaceBodyDTO)
    {
        spaceBodyDTO = await BuildSpaceBodyHierarchyDTO(spaceBodyDTO);
        return spaceBodyDTO;
    }

    public async Task<SpaceBodyDTO> BuildSpaceBodyHierarchyDTO(SpaceBodyDTO spaceBodyDTO)
    {
        var children = await spaceBodyRepository.GetAllChildrenAsync(spaceBodyDTO.Id);
        foreach (SpaceBody child in children)
        {
            SpaceBodyDTO childDTO = child.ToDTO();
            childDTO = await BuildSpaceBodyHierarchyDTO(childDTO);            
            spaceBodyDTO.Children.Add(childDTO);
        }

        return spaceBodyDTO;
    }
}
