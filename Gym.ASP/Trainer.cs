using System;
using System.Collections.Generic;

namespace Gym.ASP;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public bool Gender { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
