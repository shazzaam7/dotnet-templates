using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace MyCustomTemplate.GUI.Converters;

/// <summary>
/// Multi-value converter that formats a double using the provided format string.
/// Binding[0] = double value, Binding[1] = format string (e.g. "F0", "F2").
/// Used by SliderCard to display the current slider value.
/// </summary>
public class DoubleFormatConverter : IMultiValueConverter
{
    public static DoubleFormatConverter Instance { get; } = new DoubleFormatConverter();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count >= 1 && values[0] is double doubleValue)
        {
            string? format = values.Count >= 2 ? values[1] as string : "F0";
            return doubleValue.ToString(format ?? "F0", culture);
        }
        return string.Empty;
    }
}