namespace PointBeat.App.Entities;

public class User
{
    public string? Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public Address Address { get; set; } = null!;
}