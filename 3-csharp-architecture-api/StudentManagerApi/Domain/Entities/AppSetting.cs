namespace StudentManagerApi.Domain.Entities;

public class AppSetting
{
    public int Id { get; set; }
    public string key { get; set; } = string.Empty;
    public string value { get; set; } = string.Empty;
}