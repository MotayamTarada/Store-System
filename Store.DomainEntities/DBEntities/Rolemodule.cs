using System;
using System.Collections.Generic;

namespace Store.DomainEntities.DBEntities;

public partial class Rolemodule
{
    public int RoleModuleId { get; set; }

    public int RoleId { get; set; }

    public int ModuleId { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
