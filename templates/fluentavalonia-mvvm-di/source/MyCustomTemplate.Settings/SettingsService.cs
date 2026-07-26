using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyCustomTemplate.Settings;

/// <summary>
/// Service for managing application settings with JSON file persistence.
/// Loads and saves settings with support for backup recovery, lenient deserialization,
/// and thread-safe access.
/// </summary>
public sealed class SettingsService : ISettingsService<Settings>
{
    /// <summary>
    /// JSON serialization options for reading and writing settings files.
    /// </summary>
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// The file path to the primary settings JSON file.
    /// </summary>
    private readonly string _settingsPath;

    /// <summary>
    /// The file path to the settings backup JSON file.
    /// </summary>
    private readonly string _settingsBackupPath;

    /// <summary>
    /// Thread synchronization lock for all settings read and write operations.
    /// </summary>
    private readonly Lock _lock = new Lock();

    /// <summary>
    /// The current in-memory settings instance.
    /// </summary>
    private Settings _settings = null!;

    /// <summary>
    /// Indicates whether settings have been loaded from persistent storage.
    /// </summary>
    private bool _settingsLoaded;

    /// <summary>
    /// Gets the currently loaded settings instance.
    /// Loads settings from persistent storage if not yet initialized.
    /// </summary>
    public Settings Settings
    {
        get
        {
            if (!_settingsLoaded)
            {
                LoadSettings();
            }
            return _settings;
        }
    }

    /// <summary>
    /// Occurs when settings have been saved to persistent storage.
    /// </summary>
    public event EventHandler? SettingsChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsService"/> class.
    /// Settings are stored at <c>{AppContext.BaseDirectory}/Config/config.json</c>.
    /// </summary>
    public SettingsService()
        : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsService"/> class
    /// with custom JSON serialization options.
    /// </summary>
    /// <param name="jsonOptions">
    /// Custom JSON serialization options.
    /// If <c>null</c>, default options with <see cref="JsonStringEnumConverter"/> are used.
    /// </param>
    /// <param name="settingsPath">
    /// The full path to the settings JSON file.
    /// If <c>null</c>, defaults to <c>{AppContext.BaseDirectory}/Config/config.json</c>.
    /// </param>
    public SettingsService(JsonSerializerOptions? jsonOptions = null, string? settingsPath = null)
    {
        _jsonOptions = jsonOptions ?? new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        _settingsPath = settingsPath
                        ?? Path.Combine(AppContext.BaseDirectory, "Config", "config.json");
        _settingsBackupPath = _settingsPath + ".backup";

        string? directory = Path.GetDirectoryName(_settingsPath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    /// <summary>
    /// Loads settings from persistent storage.
    /// If the file does not exist or fails to load, default settings are returned.
    /// Invalid property values are automatically replaced with their defaults via
    /// <see cref="LenientJsonDeserializer"/>.
    /// </summary>
    /// <returns>The loaded settings instance, or default settings if loading fails.</returns>
    public Settings LoadSettings()
    {
        lock (_lock)
        {
            try
            {
                if (!File.Exists(_settingsPath))
                {
                    _settings = new Settings();
                    SaveSettingsInternal();
                    _settingsLoaded = true;
                    return _settings;
                }

                string json = File.ReadAllText(_settingsPath);
                _settings = LenientJsonDeserializer.Deserialize<Settings>(json, _jsonOptions);
                _settingsLoaded = true;
                SaveSettingsInternal();
                return _settings;
            }
            catch
            {
                try
                {
                    if (File.Exists(_settingsBackupPath))
                    {
                        string backupJson = File.ReadAllText(_settingsBackupPath);
                        _settings = LenientJsonDeserializer.Deserialize<Settings>(backupJson, _jsonOptions);
                        _settingsLoaded = true;
                        return _settings;
                    }
                }
                catch
                {
                }

                _settings = new Settings();
                _settingsLoaded = true;
                return _settings;
            }
        }
    }

    /// <summary>
    /// Asynchronously loads settings from persistent storage.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<Settings> LoadSettingsAsync()
    {
        return await Task.Run(LoadSettings);
    }

    /// <summary>
    /// Saves the current settings instance to persistent storage.
    /// </summary>
    public void SaveSettings()
    {
        SaveSettingsInternal();
    }

    /// <summary>
    /// Asynchronously saves the current settings instance to persistent storage.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SaveSettingsAsync()
    {
        await Task.Run(SaveSettingsInternal);
    }

    /// <summary>
    /// Saves the provided settings instance to persistent storage.
    /// </summary>
    /// <param name="settings">The settings instance to save.</param>
    public void SaveSettings(Settings settings)
    {
        lock (_lock)
        {
            _settings = settings;
            SaveSettingsInternal();
        }
    }

    /// <summary>
    /// Asynchronously saves the provided settings instance to persistent storage.
    /// </summary>
    /// <param name="settings">The settings instance to save.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SaveSettingsAsync(Settings settings)
    {
        await Task.Run(() => SaveSettings(settings));
    }

    /// <summary>
    /// Creates a backup of the settings file, writes the current settings to disk,
    /// and raises the <see cref="SettingsChanged"/> event.
    /// </summary>
    private void SaveSettingsInternal()
    {
        lock (_lock)
        {
            try
            {
                CreateBackup();

                string json = JsonSerializer.Serialize(_settings, _jsonOptions);
                File.WriteAllText(_settingsPath, json);

                SettingsChanged?.Invoke(this, EventArgs.Empty);
            }
            catch
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Creates a backup of the settings file if it exists.
    /// </summary>
    private void CreateBackup()
    {
        try
        {
            if (File.Exists(_settingsPath))
            {
                File.Copy(_settingsPath, _settingsBackupPath, overwrite: true);
            }
        }
        catch
        {
            // ignored
        }
    }
}