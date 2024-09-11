using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class UserReview
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ReviewId { get; set; }

    public virtual Review Review { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
