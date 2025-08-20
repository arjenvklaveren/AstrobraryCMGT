using System;
using API.Entities;

namespace API.Helpers.Types;

public class SpaceBodyFilterParams
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public bool? HasRings { get; set; }
    public SpaceBodyType? BodyType { get; set; }
}
