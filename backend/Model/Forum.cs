using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Forum
{
    public int Id { get; set; }

    public int? Creator { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual User? CreatorNavigation { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}
