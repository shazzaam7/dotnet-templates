using System.Text.Json.Serialization;
using MyCustomTemplate.Logging;

namespace MyCustomTemplate.Settings.Sections;

/// <summary>
/// Settings related to debugging and logging.
/// </summary>
public class DebugSettings
{
    /// <summary>
    /// Gets or sets the minimum logging level for log output.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="LogLevel.Info"/> in release builds and <see cref="LogLevel.Trace"/> in experimental builds.
    /// This value is applied at startup and can be changed at runtime via the Settings page.
    /// </remarks>
    [JsonPropertyName("log_level")]
#if EXPERIMENTAL_BUILD
    public LogLevel LogLevel { get; set; } = LogLevel.Trace;
#else
    public LogLevel LogLevel { get; set; } = LogLevel.Info;
#endif
}