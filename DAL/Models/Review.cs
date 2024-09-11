using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Review
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public bool IsFavorite { get; set; }

    public string? Comment { get; set; }

    public virtual ICollection<UserReview> UserReviews { get; } = new List<UserReview>();
}
