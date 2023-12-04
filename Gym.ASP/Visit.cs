using System;
using System.Collections.Generic;

namespace Gym.ASP;

public partial class Visit
{
    public int VisitId { get; set; }

    public int ClientId { get; set; }

    public DateOnly VisitDate { get; set; }

    public virtual Client Client { get; set; } = null!;
}
