using System.Text.Json.Serialization;
using MyCustomTemplate.Core.Models;

namespace MyCustomTemplate.Settings.Sections;

/// <summary>
/// Settings for the user interface, including language and theme.
/// </summary>
public class UiSettings
{
    /// <summary>
    /// Gets or sets the language code used by the application UI (e.g., "en", "de").
    /// </summary>
    /// <remarks>
    /// Defaults to <c>"en"</c> (English). Changing this at runtime reloads the localization
    /// overlay via <see cref="MyCustomTemplate.GUI.Services.LocalizationService.LoadLanguage"/>.
    /// </remarks>
    [JsonPropertyName("language")]
    public string Language { get; set; } = "en";

    /// <summary>
    /// Gets or sets the theme applied to the application UI.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="Theme.Light"/>. Changing this at runtime swaps the active
    /// resource dictionary via <see cref="MyCustomTemplate.GUI.Services.ThemeService"/>.
    /// </remarks>
    [JsonPropertyName("theme")]
    public Theme Theme { get; set; } = Theme.Light;
}