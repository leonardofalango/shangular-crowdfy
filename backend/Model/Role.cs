using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Role
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? IdForum { get; set; }

    public string Name { get; set; } = null!;

    public virtual Forum? IdForumNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
