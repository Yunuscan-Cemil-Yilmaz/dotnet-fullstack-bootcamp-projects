namespace StudentManagerApi.Domain.Entities;

public class Exam
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; } = null;

    public ICollection<ExamResult> Results { get; set; } = new List<ExamResult>();
}