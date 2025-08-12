using System;

namespace API.Entities;

public class Rating
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public RatingTarget TargetType { get; set; }
    public int TargetId { get; set; }
}

public enum RatingTarget
{
    SpaceBody,
    Astronomer
}
