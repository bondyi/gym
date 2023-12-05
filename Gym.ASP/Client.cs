namespace Gym.ASP;

public partial class Client
{
    public int ClientId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public decimal Balance { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public bool Gender { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
