using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace MyCustomTemplate.GUI.Converters;

/// <summary>
/// Returns true if the bound value is not null. Used to hide empty icons and descriptions in cards.
/// </summary>
public class NotNullConverter : IValueConverter
{
    public static NotNullConverter Instance { get; } = new NotNullConverter();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value != null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return AvaloniaProperty.UnsetValue;
    }
}