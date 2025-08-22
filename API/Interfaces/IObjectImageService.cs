using System;
using CloudinaryDotNet.Actions;

namespace API.Interfaces;

public interface IObjectImageService
{
    Task<ImageUploadResult> SetImageAsync(IFormFile file, string newPublicId);
    string GetPublicId(object obj);
}
