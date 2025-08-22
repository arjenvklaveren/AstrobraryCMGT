using System;
using API.Entities;
using API.Helpers.Types;
using API.Interfaces;

namespace API.Services;

public class AstronomerService(IAstronomerRepository astronomerRepository, IObjectImageService objectImageService) : IAstronomerService
{
    public async Task<IReadOnlyList<Astronomer>> GetAllAstronomersAsync(AstronomerFilterParams filterParams)
    {
        var astronomers = await astronomerRepository.GetAllAsync(filterParams);
        return astronomers.ToList();
    }

    public async Task<Astronomer?> GetAstronomerByIdAsync(int id)
    {
        return await astronomerRepository.GetByIdAsync(id);
    }

    public async Task<int?> AddAstronomerAsync(Astronomer astronomer)
    {
        astronomer = await astronomerRepository.AddAsync(astronomer);
        return astronomer.Id;
    }

    public async Task<Astronomer> UpdateAstronomerAsync(Astronomer astronomer)
    {
        return await astronomerRepository.UpdateAsync(astronomer);
    }

    public async Task RemoveAstronomerAsync(int id)
    {
        await astronomerRepository.RemoveAsync(id);
    }

    public async Task<string> SetAstronomerImage(IFormFile file, int id)
    {
        var astronomer = await astronomerRepository.GetByIdAsync(id);
        if (astronomer == null) return "";

        var publicImageId = objectImageService.GetPublicId(astronomer);
        var imageUploadResult = await objectImageService.SetImageAsync(file, publicImageId);
        if (imageUploadResult.Error != null) return astronomer.ImageUrl!;

        astronomer.ImageUrl = imageUploadResult.SecureUrl.AbsoluteUri;
        await astronomerRepository.UpdateAsync(astronomer);
        return imageUploadResult.SecureUrl.AbsoluteUri;
    }
}
