using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class User
{
    public int Id { get; set; }

    public string? Completename { get; set; }

    public string? Username { get; set; }

    public string? Photo { get; set; }
    public string? Salt { get; set; }

    public DateTime? BornDate { get; set; }

    public string? Mail { get; set; }

    public int? IsAuth { get; set; }

    public virtual ICollection<Forum> IdForums { get; set; } = new List<Forum>();

    public virtual ICollection<Post> IdPosts { get; set; } = new List<Post>();
}
