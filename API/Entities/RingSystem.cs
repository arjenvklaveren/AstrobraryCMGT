using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities;

public class RingSystem
{
    //Ring properties
    public int Id { get; set; }
    public int RingDistance { get; set; }
    public int RingSize { get; set; }
    public required string RingMainColorHex { get; set; }
    public string? RingSubColorHex { get; set; }
    public string? RingDetailColorHex { get; set; }

    public int SpaceBodyId { get; set; }
    [ForeignKey(nameof(SpaceBodyId))]
    [JsonIgnore]
    public SpaceBody SpaceBody { get; set; } = null!;
}
