using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class SongGenre
{
    public int Id { get; set; }

    public int SongId { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
