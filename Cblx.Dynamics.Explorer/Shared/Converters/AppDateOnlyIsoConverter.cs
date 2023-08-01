using MudBlazor;

namespace Cblx.Dynamics.Explorer.Shared.Converters;

internal class AppDateOnlyIsoConverter : DefaultConverter<DateOnly?>
{
    /// <summary>
    /// ISO formatted dates
    /// yyyy-MM-dd
    /// yyyy-MM-dd HH:mm:ss
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override DateOnly? ConvertFromString(string value)
    {
        if (string.IsNullOrEmpty(value)) return null;

        try
        {
            return DateOnly.Parse(value);
        }
        catch (FormatException)
        {
            UpdateGetError("Invalid date");
        }
        return null;
    }

    protected override string? ConvertToString(DateOnly? arg)
    {
        if (arg is null) { return null; }
        return arg.Value.ToString("yyyy-MM-dd");
    }
}
