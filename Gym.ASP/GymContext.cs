using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gym.ASP;

public partial class GymContext : DbContext
{
    public GymContext()
    {
    }

    public GymContext(DbContextOptions<GymContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=postgres;Database=gym;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("pk_client");

            entity.ToTable("client");

            entity.HasIndex(e => e.ClientId, "client_pk").IsUnique();

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Balance)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("balance");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(16)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HashPassword)
                .HasMaxLength(100)
                .HasColumnName("hash_password");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.LastName)
                .HasMaxLength(16)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(13)
                .HasColumnName("phone_number");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("pk_feedback");

            entity.ToTable("feedback");

            entity.HasIndex(e => e.FeedbackId, "feedback_pk").IsUnique();

            entity.HasIndex(e => e.ClientId, "leaves_fk");

            entity.HasIndex(e => e.TrainerId, "receives_fk");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Message)
                .HasMaxLength(1000)
                .HasColumnName("message");
            entity.Property(e => e.Rating)
                .HasPrecision(10, 1)
                .HasColumnName("rating");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_feedback_leaves_client");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_feedback_receives_trainer");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("pk_lesson");

            entity.ToTable("lesson");

            entity.HasIndex(e => e.TrainerId, "conducts_fk");

            entity.HasIndex(e => e.LessonId, "lesson_pk").IsUnique();

            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lesson_conducts_trainer");

            entity.HasMany(d => d.Clients).WithMany(p => p.Lessons)
                .UsingEntity<Dictionary<string, object>>(
                    "Attend",
                    r => r.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_attends_attends2_client"),
                    l => l.HasOne<Lesson>().WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_attends_attends_lesson"),
                    j =>
                    {
                        j.HasKey("LessonId", "ClientId").HasName("pk_attends");
                        j.ToTable("attends");
                        j.HasIndex(new[] { "ClientId" }, "attends2_fk");
                        j.HasIndex(new[] { "LessonId" }, "attends_fk");
                        j.HasIndex(new[] { "LessonId", "ClientId" }, "attends_pk").IsUnique();
                        j.IndexerProperty<int>("LessonId").HasColumnName("lesson_id");
                        j.IndexerProperty<int>("ClientId").HasColumnName("client_id");
                    });
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("pk_payment");

            entity.ToTable("payment");

            entity.HasIndex(e => e.ServiceId, "consists of_FK");

            entity.HasIndex(e => e.ClientId, "makes_fk");

            entity.HasIndex(e => e.PaymentId, "payment_pk").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_payment_makes_client");

            entity.HasOne(d => d.Service).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_PAYMENT_CONSISTS _SERVICE");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("pk_service");

            entity.ToTable("service");

            entity.HasIndex(e => e.ServiceId, "service_pk").IsUnique();

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("pk_subscription");

            entity.ToTable("subscription");

            entity.HasIndex(e => e.ClientId, "has_fk");

            entity.HasIndex(e => e.SubscriptionId, "subscription_pk").IsUnique();

            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");

            entity.HasOne(d => d.Client).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_subscrip_has_client");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("pk_trainer");

            entity.ToTable("trainer");

            entity.HasIndex(e => e.TrainerId, "trainer_pk").IsUnique();

            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(16)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HashPassword)
                .HasMaxLength(100)
                .HasColumnName("hash_password");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(16)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(13)
                .HasColumnName("phone_number");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("pk_visit");

            entity.ToTable("visit");

            entity.HasIndex(e => e.ClientId, "uses_fk");

            entity.HasIndex(e => e.VisitId, "visit_pk").IsUnique();

            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.VisitDate).HasColumnName("visit_date");

            entity.HasOne(d => d.Client).WithMany(p => p.Visits)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_visit_uses_client");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
