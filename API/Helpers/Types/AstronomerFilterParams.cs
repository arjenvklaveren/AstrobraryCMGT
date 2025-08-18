using System;
using API.Entities;

namespace API.Helpers.Types;

public class AstronomerFilterParams
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public bool? IsMarried { get; set; }
    public AstronomerOccupation? Occupation { get; set; }
}
