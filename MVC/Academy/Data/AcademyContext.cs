using System;
using System.Collections.Generic;
using Academy.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Data;

public partial class AcademyContext : DbContext
{
    public AcademyContext(DbContextOptions<AcademyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<DaysOff> DaysOffs { get; set; }

    public virtual DbSet<Direction> Directions { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasOne(d => d.LessonNavigation).WithMany(p => p.Attendances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Schedule");

            entity.HasOne(d => d.StudentNavigation).WithMany(p => p.Attendances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Students");
        });

        modelBuilder.Entity<DaysOff>(entity =>
        {
            entity.HasOne(d => d.HolidayNavigation).WithMany(p => p.DaysOffs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DaysOFF_Holidays");
        });

        modelBuilder.Entity<Direction>(entity =>
        {
            entity.Property(e => e.DirectionId).ValueGeneratedOnAdd();

            entity.HasMany(d => d.Disciplines).WithMany(p => p.Directions)
                .UsingEntity<Dictionary<string, object>>(
                    "DisciplinesDirectionsRelation",
                    r => r.HasOne<Discipline>().WithMany()
                        .HasForeignKey("Discipline")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DisciplinesDirectionsRelation_Disciplines"),
                    l => l.HasOne<Direction>().WithMany()
                        .HasForeignKey("Direction")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DisciplinesDirectionsRelation_Directions"),
                    j =>
                    {
                        j.HasKey("Direction", "Discipline");
                        j.ToTable("DisciplinesDirectionsRelation");
                        j.IndexerProperty<byte>("Direction")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("direction");
                        j.IndexerProperty<short>("Discipline").HasColumnName("discipline");
                    });
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasMany(d => d.DependentDiscipline1s).WithMany(p => p.Disciplines)
                .UsingEntity<Dictionary<string, object>>(
                    "DependentDiscipline",
                    r => r.HasOne<Discipline>().WithMany()
                        .HasForeignKey("DependentDiscipline1")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DependentDisciplines_Disciplines1"),
                    l => l.HasOne<Discipline>().WithMany()
                        .HasForeignKey("Discipline")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DependentDisciplines_Disciplines"),
                    j =>
                    {
                        j.HasKey("Discipline", "DependentDiscipline1");
                        j.ToTable("DependentDisciplines");
                        j.IndexerProperty<short>("Discipline").HasColumnName("discipline");
                        j.IndexerProperty<short>("DependentDiscipline1").HasColumnName("dependent_discipline");
                    });

            entity.HasMany(d => d.Disciplines).WithMany(p => p.DependentDiscipline1s)
                .UsingEntity<Dictionary<string, object>>(
                    "DependentDiscipline",
                    r => r.HasOne<Discipline>().WithMany()
                        .HasForeignKey("Discipline")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DependentDisciplines_Disciplines"),
                    l => l.HasOne<Discipline>().WithMany()
                        .HasForeignKey("DependentDiscipline1")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DependentDisciplines_Disciplines1"),
                    j =>
                    {
                        j.HasKey("Discipline", "DependentDiscipline1");
                        j.ToTable("DependentDisciplines");
                        j.IndexerProperty<short>("Discipline").HasColumnName("discipline");
                        j.IndexerProperty<short>("DependentDiscipline1").HasColumnName("dependent_discipline");
                    });

            entity.HasMany(d => d.DisciplinesNavigation).WithMany(p => p.RequiredDiscipline1s)
                .UsingEntity<Dictionary<string, object>>(
                    "RequiredDiscipline",
                    r => r.HasOne<Discipline>().WithMany()
                        .HasForeignKey("Discipline")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RequiredDisciplines_Disciplines"),
                    l => l.HasOne<Discipline>().WithMany()
                        .HasForeignKey("RequiredDiscipline1")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RequiredDisciplines_Disciplines1"),
                    j =>
                    {
                        j.HasKey("Discipline", "RequiredDiscipline1");
                        j.ToTable("RequiredDisciplines");
                        j.IndexerProperty<short>("Discipline").HasColumnName("discipline");
                        j.IndexerProperty<short>("RequiredDiscipline1").HasColumnName("required_discipline");
                    });

            entity.HasMany(d => d.RequiredDiscipline1s).WithMany(p => p.DisciplinesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "RequiredDiscipline",
                    r => r.HasOne<Discipline>().WithMany()
                        .HasForeignKey("RequiredDiscipline1")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RequiredDisciplines_Disciplines1"),
                    l => l.HasOne<Discipline>().WithMany()
                        .HasForeignKey("Discipline")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RequiredDisciplines_Disciplines"),
                    j =>
                    {
                        j.HasKey("Discipline", "RequiredDiscipline1");
                        j.ToTable("RequiredDisciplines");
                        j.IndexerProperty<short>("Discipline").HasColumnName("discipline");
                        j.IndexerProperty<short>("RequiredDiscipline1").HasColumnName("required_discipline");
                    });
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasOne(d => d.DisciplineNavigation).WithMany(p => p.Exams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exams_Disciplines");

            entity.HasOne(d => d.StudentNavigation).WithMany(p => p.Exams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exams_Students");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => new { e.Student, e.Lesson }).HasName("PK_Grades_1");

            entity.HasOne(d => d.LessonNavigation).WithMany(p => p.Grades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Schedule");

            entity.HasOne(d => d.StudentNavigation).WithMany(p => p.Grades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Students");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.GroupName).IsFixedLength();

            entity.HasOne(d => d.DirectionNavigation).WithMany(p => p.Groups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_Directions");

            entity.HasMany(d => d.Disciplines).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "CompleteDiscipline",
                    r => r.HasOne<Discipline>().WithMany()
                        .HasForeignKey("Discipline")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CompleteDisciplines_Disciplines"),
                    l => l.HasOne<Group>().WithMany()
                        .HasForeignKey("Group")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CompleteDisciplines_Groups"),
                    j =>
                    {
                        j.HasKey("Group", "Discipline");
                        j.ToTable("CompleteDisciplines");
                        j.IndexerProperty<int>("Group").HasColumnName("group");
                        j.IndexerProperty<short>("Discipline").HasColumnName("discipline");
                    });
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasOne(d => d.DisciplineNavigation).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Disciplines");

            entity.HasOne(d => d.GroupNavigation).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Groups");

            entity.HasOne(d => d.TeacherNavigation).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Teachers");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.GroupNavigation).WithMany(p => p.Students).HasConstraintName("FK_Students_Groups");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasMany(d => d.Disciplines).WithMany(p => p.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeachersDisciplinesRelation",
                    r => r.HasOne<Discipline>().WithMany()
                        .HasForeignKey("Discipline")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeachersDisciplinesRelation_Disciplines"),
                    l => l.HasOne<Teacher>().WithMany()
                        .HasForeignKey("Teacher")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeachersDisciplinesRelation_Teachers"),
                    j =>
                    {
                        j.HasKey("Teacher", "Discipline");
                        j.ToTable("TeachersDisciplinesRelation");
                        j.IndexerProperty<short>("Teacher").HasColumnName("teacher");
                        j.IndexerProperty<short>("Discipline").HasColumnName("discipline");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
