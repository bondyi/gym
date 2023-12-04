using System;
using System.Collections.Generic;

namespace Gym.ASP;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int TrainerId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Trainer Trainer { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
