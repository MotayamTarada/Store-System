using System;
using System.Collections.Generic;

namespace Store.DomainEntities.DBEntities;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string BriefDescription { get; set; } = null!;

    public double Price { get; set; }

    public string Remark { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Image { get; set; }

    public int? Id { get; set; }

    public virtual Acayear? IdNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
