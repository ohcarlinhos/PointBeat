namespace PointBeat.App.Validators;

public class ValidatorUtils
{
    public static bool BeAValidDate(string data)
    {
        DateTime date;
        return DateTime.TryParse(data, out date);
    }
}