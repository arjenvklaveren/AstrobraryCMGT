using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

public class RingSystem
{
    //Ring properties
    public int Id { get; set; }
    public int? RingDistance { get; set; }
    public int? RingSize { get; set; }
    public string? RingMainColorHex { get; set; }
    public string? RingSubColorHex { get; set; }
    public string? RingDetailColorHex { get; set; }

    public int SpaceBodyId { get; set; }
    [ForeignKey(nameof(SpaceBodyId))]
    public SpaceBody SpaceBody { get; set; } = null!;
}
