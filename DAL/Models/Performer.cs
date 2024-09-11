using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Performer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? YearOfBirth { get; set; }

    public virtual ICollection<Song> Songs { get; } = new List<Song>();
}
