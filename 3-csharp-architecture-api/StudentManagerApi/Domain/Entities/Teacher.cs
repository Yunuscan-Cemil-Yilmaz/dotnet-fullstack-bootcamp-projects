namespace StudentManagerApi.Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}