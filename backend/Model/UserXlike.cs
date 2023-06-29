using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class UserXlike
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? IdPost { get; set; }

    public virtual Post? IdPostNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
