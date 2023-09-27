using MudBlazor;

namespace Cblx.Dynamics.Explorer.Shared.Converters;

internal class AppDateTimeIsoConverter : DefaultConverter<DateTime?>
{
    public bool DateOnly { get; set; }

    /// <summary>
    /// ISO formatted dates
    /// yyyy-MM-dd
    /// yyyy-MM-dd HH:mm:ss
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override DateTime? ConvertFromString(string value)
    {
        if (string.IsNullOrEmpty(value)) return null;

        try
        {
            return DateTime.Parse(value);
        }
        catch (FormatException)
        {
            UpdateGetError("Invalid date");
        }
        return null;
    }

    protected override string? ConvertToString(DateTime? arg)
    {
        if (arg is null) { return null; }
        return DateOnly ? arg.Value.ToString("yyyy-MM-dd") : arg.Value.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
