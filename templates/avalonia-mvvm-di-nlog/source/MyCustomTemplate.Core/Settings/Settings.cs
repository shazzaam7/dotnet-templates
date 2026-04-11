using System.Text.Json.Serialization;
using MyCustomTemplate.Core.Settings.Sections;

namespace MyCustomTemplate.Core.Settings;

/// <summary>
/// Manages application-specific settings with loading, saving, and access functionality.
/// </summary>
public class Settings
{
    /// <summary>
    /// Debugging and logging settings
    /// </summary>
    [JsonPropertyName("debug")]
    public DebugSettings Debug { get; set; } = new DebugSettings();
}
