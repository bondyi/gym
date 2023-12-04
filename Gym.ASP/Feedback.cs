using System;
using System.Collections.Generic;

namespace Gym.ASP;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int ClientId { get; set; }

    public int TrainerId { get; set; }

    public decimal Rating { get; set; }

    public string Message { get; set; } = null!;

    public DateOnly CreationDate { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Trainer Trainer { get; set; } = null!;
}
