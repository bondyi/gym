using System;
using System.Collections.Generic;

namespace Gym.ASP;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
