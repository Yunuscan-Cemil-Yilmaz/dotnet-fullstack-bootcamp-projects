namespace PollCraft.Models;

public interface ITimestampEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}