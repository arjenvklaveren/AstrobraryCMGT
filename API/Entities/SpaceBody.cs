using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

public class SpaceBody
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

    //Navigation properties
    [ForeignKey(nameof(DiscovererId))]
    public Astronomer Discoverer { get; set; } = null!;

    public RingSystem? RingSystem { get; set; }
}

public enum SpaceBodyType
{
    Moon,
    Planet,
    Star,
}