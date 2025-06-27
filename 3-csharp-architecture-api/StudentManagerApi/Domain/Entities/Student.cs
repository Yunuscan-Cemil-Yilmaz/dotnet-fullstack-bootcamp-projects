namespace StudentManagerApi.Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
}