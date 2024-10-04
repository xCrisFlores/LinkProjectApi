using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class LinkProjectBddContext : DbContext
{
    public LinkProjectBddContext()
    {
    }

    public LinkProjectBddContext(DbContextOptions<LinkProjectBddContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adviser> Advisers { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=LinkProjectBDD;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adviser>(entity =>
        {
            entity.HasKey(e => e.AdviserCode).HasName("PK__Adviser__D7074BC65233E8DC");

            entity.ToTable("Adviser");

            entity.Property(e => e.AdviserCode)
                .ValueGeneratedNever()
                .HasColumnName("adviser_code");
            entity.Property(e => e.Division)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("division");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.StudentCode).HasName("PK__Schedule__DBBE77C8357E03BF");

            entity.ToTable("Schedule");

            entity.Property(e => e.StudentCode)
                .ValueGeneratedNever()
                .HasColumnName("Student_code");
            entity.Property(e => e.Day)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("day");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.StudentCode).HasName("PK__Skills__6DF33C44D2C9A907");

            entity.Property(e => e.StudentCode)
                .ValueGeneratedNever()
                .HasColumnName("student_code");
            entity.Property(e => e.Skill1)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("skill");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentCode).HasName("PK__Student__6DF33C44D35F8367");

            entity.ToTable("Student");

            entity.Property(e => e.StudentCode)
                .ValueGeneratedNever()
                .HasColumnName("student_code");
            entity.Property(e => e.Biography)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("biography");
            entity.Property(e => e.Lab)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lab");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__User__357D4CF8865CB3D1");

            entity.ToTable("User");

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Path)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("path");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
