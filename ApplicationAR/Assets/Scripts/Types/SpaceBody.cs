using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpaceBody
{
    //General properties
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DiscoveryDate { get; set; }
    public int DiscovererId { get; set; }
    public int Age { get; set; }
    public string ImageUrl { get; set; }
    public int ParentId { get; set; }
    public SpaceBodyType Type { get; set; }
    public string MainInfo { get; set; }
    public string SubInfo { get; set; }

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
    public string MainColorHex { get; set; }
    public string SubColorHex { get; set; }

    public RingSystem RingSystem { get; set; }

    public List<SpaceBody> Children { get; set; }
}

public enum SpaceBodyType
{
    Moon,
    Planet,
    Star,
}
