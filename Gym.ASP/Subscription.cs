using System;
using System.Collections.Generic;

namespace Gym.ASP;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int ClientId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly PurchaseDate { get; set; }

    public virtual Client Client { get; set; } = null!;
}
