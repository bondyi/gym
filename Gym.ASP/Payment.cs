using System;
using System.Collections.Generic;

namespace Gym.ASP;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? ClientId { get; set; }

    public int? ServiceId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Service? Service { get; set; }
}
