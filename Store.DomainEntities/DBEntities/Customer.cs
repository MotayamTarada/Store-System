using System;
using System.Collections.Generic;

namespace Store.DomainEntities.DBEntities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string EMail { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Roleuser> Roleusers { get; } = new List<Roleuser>();
}
