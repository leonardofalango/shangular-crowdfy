using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Post
{
    public int Id { get; set; }

    public int? Author { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Anex { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IdPost { get; set; }

    public virtual Post? IdPostNavigation { get; set; }

    public virtual ICollection<Post> InverseIdPostNavigation { get; set; } = new List<Post>();
}
