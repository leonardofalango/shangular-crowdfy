using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Post
{
    public int Id { get; set; }

    public int? Author { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Crowds { get; set; }

    public int? Comments { get; set; }

    public int? IdPost { get; set; }

    public int? IdForum { get; set; }

    public virtual Forum? IdForumNavigation { get; set; }

    public virtual Post? IdPostNavigation { get; set; }

    public virtual ICollection<Post> InverseIdPostNavigation { get; set; } = new List<Post>();

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}
