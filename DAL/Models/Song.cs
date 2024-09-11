using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Song
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Tempo { get; set; }

    public string? Melody { get; set; }

    public string? Language { get; set; }

    public int? YearOfRelease { get; set; }

    public int PerformerId { get; set; }

    public virtual Performer Performer { get; set; } = null!;

    public virtual ICollection<SongGenre> SongGenres { get; } = new List<SongGenre>();
}
