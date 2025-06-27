using Microsoft.EntityFrameworkCore;
using StudentManagerApi.Domain.Entities;

namespace StudentManagerApi.Infrastructure.Config;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Exam> Exams => Set<Exam>();
    public DbSet<ExamResult> ExamResults => Set<ExamResult>();
    public DbSet<AppSetting> AppSettings => Set<AppSetting>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Student - Lesson: many-to-many
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Lessons)
            .WithMany(l => l.Students)
            .UsingEntity(j => j.ToTable("StudentLessons"));

        // 2. Lesson - Exam: one-to-many
        modelBuilder.Entity<Lesson>()
            .HasMany(l => l.Exams)
            .WithOne(e => e.Lesson)
            .HasForeignKey(e => e.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        // 3. Exam - ExamResult: one-to-many
        modelBuilder.Entity<Exam>()
            .HasMany(e => e.Results)
            .WithOne(er => er.Exam)
            .HasForeignKey(er => er.ExamId)
            .OnDelete(DeleteBehavior.Cascade);

        // 4. ExamResult - Student: many-to-one
        modelBuilder.Entity<ExamResult>()
            .HasOne(er => er.Student)
            .WithMany(s => s.ExamResults)
            .HasForeignKey(er => er.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}