namespace StudentManagerApi.Domain.Entities;

public class ExamResult
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    public double Score { get; set; }
}