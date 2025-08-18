using System;

namespace API.Entities;

public class Astronomer
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string BirthPlace { get; set; }
    public string? ImageUrl { get; set; }
    public AstronomerOccupation Occupation { get; set; }
    public string? Description { get; set; }
    public bool Married { get; set; }
    public required string Gender { get; set; }
    public int TelescopeAmount { get; set; }
}

public enum AstronomerOccupation
{
    Scientist,
    Hobbyist,
    Astronaut,
    Researcher
}
