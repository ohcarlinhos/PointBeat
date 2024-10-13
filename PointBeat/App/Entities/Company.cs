namespace PointBeat.App.Entities;

public class Company
{
    public string? Id { get; set; }
    public string Name { get; set; } = "";
    public Address Address { get; set; } = null!;
}