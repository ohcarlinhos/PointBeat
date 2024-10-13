namespace PointBeat.App.Entities;

public class Point
{
    public string? Id { get; set; }
    public DateTime Hour { get; set; }
    public string UserId { get; set; } = "";
    public string CompanyId { get; set; } = "";
}