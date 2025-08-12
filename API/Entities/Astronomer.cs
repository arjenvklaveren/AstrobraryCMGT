using System;

namespace API.Entities;

public class Astronomer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ImageUrl { get; set; }
    public AstronomerOccupation Occupation { get; set; }
}

public enum AstronomerOccupation
{
    Scientist,
    Amateur,
    Astronaut,
    Casual
}
