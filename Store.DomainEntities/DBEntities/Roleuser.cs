using System;
using System.Collections.Generic;

namespace Store.DomainEntities.DBEntities;

public partial class Roleuser
{
    public int RoleUserId { get; set; }

    public int RoleId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Role Role { get; set; } = null!;
}
