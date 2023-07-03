using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class UserXforum
{
    public int IdUser { get; set; }

    public int IdForum { get; set; }

    public int Id { get; set; }

    public virtual Forum IdForumNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
