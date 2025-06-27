using StudentManagerApi.Domain.Entities;
using StudentManagerApi.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace StudentManagerApi.Infrastructure.Seed;

public class SeedService
{
    private readonly StudentDbContext _context;

    public SeedService(StudentDbContext context)
    {
        _context = context;
    }
    private readonly double currentSeedVersion = 1.1;
    public async Task SeedAsync()
    {
        string? seedVersionValueString = await _context.AppSettings
            .Where(s => s.key == "SeedVersion")
            .Select(s => s.value)
            .FirstOrDefaultAsync();

        double seedVersion = seedVersionValueString != null && double.TryParse(seedVersionValueString, out var parsedValue) 
            ? parsedValue 
            : 0.0;

        if (seedVersion == 0.0)
        {
            var newSetting = new AppSetting
            {
                key = "SeedVersion",
                value = "1.0"
            };
            await _context.AppSettings.AddAsync(newSetting);
            await _context.SaveChangesAsync();
        }

        if (seedVersion < currentSeedVersion)
        {
            Console.WriteLine("Old seed detected. Truncating all data...");
            try
            {

                // Truncate all tables
                var conn = _context.Database.GetDbConnection();
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    TRUNCATE TABLE ExamResults;
                    TRUNCATE TABLE Exams;
                    TRUNCATE TABLE StudentLessons;
                    TRUNCATE TABLE Lessons;
                    TRUNCATE TABLE Students;
                    TRUNCATE TABLE Teachers;
                    SET FOREIGN_KEY_CHECKS = 1;
                ";
                await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();

                await _context.AppSettings
                    .Where(s => s.key == "SeedVersion")
                    .ExecuteUpdateAsync(s => s.SetProperty(p => p.value, currentSeedVersion.ToString()));

                Console.WriteLine("Database truncated and seed version updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error truncating database: {ex.Message}");
                return;
            }
        }
        else if (
            await _context.Students.AnyAsync() ||
            await _context.Lessons.AnyAsync() ||
            await _context.Teachers.AnyAsync() ||
            await _context.Exams.AnyAsync() ||
            await _context.ExamResults.AnyAsync()
        )
        {
            Console.WriteLine("Database already seeded.");
            return;
        }
        else
        {
            Console.WriteLine("Seeding database...");
        }

        // Seed Teachers
        var teachers = new List<Teacher>
        {
            new Teacher {FullName = "Ali Demir"},
            new Teacher {FullName = "Zeynep Yılmaz"},
        };
        await _context.Teachers.AddRangeAsync(teachers);

        // seed Lessons
        var lessons = new List<Lesson>
        {
            new Lesson {Name = "Matematik", Teacher = teachers[0]},
            new Lesson {Name = "Fizik", Teacher = teachers[0]},
            new Lesson {Name = "Biyoloji", Teacher = teachers[1]},
            new Lesson {Name = "Kimya", Teacher = teachers[1]},
        };
        await _context.Lessons.AddRangeAsync(lessons);

        // seed Students
        var students = new List<Student>();
        for (int i = 0; i < 25; i++)
        {
            students.Add(new Student { FullName = $"Öğrenci {i + 1}" });
        }
        await _context.Students.AddRangeAsync(students);

        // seed Lessons
        var random = new Random();
        foreach (var student in students)
        {
            var selectedLessons = lessons.OrderBy(_ => random.Next()).Take(3).ToList();
            foreach (var sl in selectedLessons)
            {
                student.Lessons.Add(sl);
            }
        }

        // seed Exams
        var exams = new List<Exam>();
        foreach (var l in lessons)
        {
            exams.Add(new Exam
            {
                Title = $"{l.Name} Vize",
                Lesson = l,
                Date = DateTime.Now.AddDays(random.Next(1, 30))
            });

            exams.Add(new Exam
            {
                Title = $"{l.Name} Final",
                Lesson = l,
                Date = DateTime.Now.AddDays(random.Next(60, 90))
            });
        }
        await _context.Exams.AddRangeAsync(exams);

        var examResults = new List<ExamResult>();
        foreach (var student in students)
        {
            var studentLessons = student.Lessons;
            foreach (var sl in studentLessons)
            {
                var lessonExams = exams.Where(e => e.LessonId == sl.Id).ToList();
                foreach (var le in lessonExams)
                {
                    examResults.Add(new ExamResult
                    {
                        Exam = le,
                        Student = student,
                        Score = random.Next(5, 101)
                    });
                }
            }
        }
        await _context.ExamResults.AddRangeAsync(examResults);

        await _context.SaveChangesAsync();
        Console.WriteLine("Database seeding completed.");
    }

}