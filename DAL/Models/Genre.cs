using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? DebutYear { get; set; }

    public virtual ICollection<SongGenre> SongGenres { get; } = new List<SongGenre>();
}
