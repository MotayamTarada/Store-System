using System;
using System.Collections.Generic;

namespace Store.DomainEntities.DBEntities;

public partial class Module
{
    public int ModuleId { get; set; }

    public string ModuleName { get; set; } = null!;

    public virtual ICollection<Rolemodule> Rolemodules { get; } = new List<Rolemodule>();
}
