using System;
using System.Collections.Generic;

namespace Store.DomainEntities.DBEntities;

public partial class Acayear
{
    public int Id { get; set; }

    public string AcaYearcol { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
