namespace StudentManagerApi.Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
}