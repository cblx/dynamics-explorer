﻿using MudBlazor;

namespace Cblx.Dynamics.Explorer.Shared.Converters;

internal class AppDateOnlyIsoConverter : DefaultConverter<DateOnly?>
{
    /// <summary>
    /// Espera-se datas em formato ISO
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
            UpdateGetError("Data inválida");
        }
        return null;
    }
}
