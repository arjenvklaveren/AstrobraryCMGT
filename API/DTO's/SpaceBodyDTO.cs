using System;
using API.Entities;

namespace API.DTO_s;

public class SpaceBodyDTO
{
    //General properties
    public int? Id { get; set; }
    public required string Name { get; set; }
    public required DateOnly DiscoveryDate { get; set; }
    public int DiscovererId { get; set; }
    public int Age { get; set; }
    public string? ImageUrl { get; set; }
    public int? ParentId { get; set; }
    public required SpaceBodyType Type { get; set; }
    public required string MainInfo { get; set; }
    public string? SubInfo { get; set; }

    //Characteristic properties
    public int Mass { get; set; }
    public int Luminosity { get; set; }
    public int Diameter { get; set; }
    public int Velocity { get; set; }
    public int Temperature { get; set; }
    public int DistanceFromParent { get; set; }
    public int RotationAngle { get; set; }
    public int RotationSpeed { get; set; }
    public int AtmosphereThickness { get; set; }
    public required string MainColorHex { get; set; }
    public string? SubColorHex { get; set; }

    public RingSystem? RingSystem { get; set; }

    public List<SpaceBodyDTO> Children { get; set; } = [];
}
