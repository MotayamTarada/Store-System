using System;
using System.Collections.Generic;

namespace Store.DomainEntities.DBEntities;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
