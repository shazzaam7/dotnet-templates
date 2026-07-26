using System.Text.Json.Serialization;
using MyCustomTemplate.Core.Models;

namespace MyCustomTemplate.Settings.Sections;

/// <summary>
/// Subsection for UI settings
/// </summary>
public class UiSettings
{
    /// <summary>
    /// <para>Language used by the application UI</para>
    /// Default Language = English
    /// </summary>
    [JsonPropertyName("language")]
    public string Language { get; set; } = "en";

    /// <summary>
    /// <para>Theme used by the application UI</para>
    /// Default Theme = Light
    /// </summary>
    [JsonPropertyName("theme")]
    public Theme Theme { get; set; } = Theme.Light;
}