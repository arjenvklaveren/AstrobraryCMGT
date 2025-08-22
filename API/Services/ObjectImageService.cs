using System;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services;

public class ObjectImageService : IObjectImageService
{
    private readonly Cloudinary _cloudinary;

    public ObjectImageService(IOptions<CloudinarySettings> config)
    {
        var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> SetImageAsync(IFormFile file, string newPublicId)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = newPublicId,
                Overwrite = true,
                Transformation = new Transformation().Height(750).Width(750).Crop("fill").Gravity("auto"),
                Folder = "da-ang20"
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public string GetPublicId(object obj)
    {
        var type = obj.GetType();
        if (type != typeof(Astronomer) && type != typeof(SpaceBody)) throw new ArgumentException("Wrong object");

        var idProp = type.GetProperty("Id");
        if (idProp == null) throw new ArgumentException("No id");

        var id = idProp.GetValue(obj);

        return $"{type.Name}_{id}";
    }
}
