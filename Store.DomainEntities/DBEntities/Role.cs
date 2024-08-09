using System;
using System.Collections.Generic;

namespace Store.DomainEntities.DBEntities;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Rolemodule> Rolemodules { get; } = new List<Rolemodule>();

    public virtual ICollection<Roleuser> Roleusers { get; } = new List<Roleuser>();
}
