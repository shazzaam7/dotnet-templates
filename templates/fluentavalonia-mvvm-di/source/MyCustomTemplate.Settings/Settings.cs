using System.Text.Json.Serialization;
using MyCustomTemplate.Settings.Sections;

namespace MyCustomTemplate.Settings;

/// <summary>
/// The root settings class for the application.
/// Add settings sections as properties to extend this class.
/// </summary>
/// <remarks>
/// <para>
/// Each settings section should be a public class with a parameterless constructor and
/// properties decorated with <see cref="JsonPropertyNameAttribute"/> to control JSON key names.
/// </para>
/// </remarks>
public class Settings
{
    [JsonPropertyName("debug")]
    public DebugSettings Debug { get; set; } = new DebugSettings();

    [JsonPropertyName("ui")]
    public UiSettings Ui { get; set; } = new UiSettings();
}