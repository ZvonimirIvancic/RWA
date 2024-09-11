using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI.Dtos;

public class SongDto
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Tempo { get; set; }

    public string? Melody { get; set; }

    public string? Language { get; set; }

    public int? YearOfRelease { get; set; }

    public int PerformerId { get; set; }


}
