using System;
using API.DTO_s;
using API.Entities;

namespace API.Helpers.Extensions;

public static class SpaceBodyExtensions
{
    public static SpaceBodyDTO ToDTO(this SpaceBody spaceBody)
    {
        return new SpaceBodyDTO
        {
            Id = spaceBody.Id,
            Name = spaceBody.Name,
            DiscoveryDate = spaceBody.DiscoveryDate,
            DiscovererId = spaceBody.DiscovererId,
            Age = spaceBody.Age,
            ImageUrl = spaceBody.ImageUrl,
            ParentId = spaceBody.ParentId,
            Type = spaceBody.Type,
            MainInfo = spaceBody.MainInfo,
            SubInfo = spaceBody.SubInfo,

            Mass = spaceBody.Mass,
            Luminosity = spaceBody.Luminosity,
            Diameter = spaceBody.Diameter,
            Velocity = spaceBody.Velocity,
            Temperature = spaceBody.Temperature,
            DistanceFromParent = spaceBody.DistanceFromParent,
            RotationAngle = spaceBody.RotationAngle,
            RotationSpeed = spaceBody.RotationSpeed,
            AtmosphereThickness = spaceBody.AtmosphereThickness,
            MainColorHex = spaceBody.MainColorHex,
            SubColorHex = spaceBody.SubColorHex,
        };
    }
}
