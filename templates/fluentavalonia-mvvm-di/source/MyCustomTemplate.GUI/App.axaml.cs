using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCustomTemplate.GUI.Services;
using MyCustomTemplate.GUI.ViewModels;
using MyCustomTemplate.GUI.Views;
using MyCustomTemplate.Logging;
using MyCustomTemplate.Settings;
using LogLevel = MyCustomTemplate.Logging.LogLevel;

namespace MyCustomTemplate.GUI;

public partial class App : Application
{
    /// <summary>
    /// Desktop instance
    /// </summary>
    public static readonly IClassicDesktopStyleApplicationLifetime? Desktop = Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

    /// <summary>
    /// Main Window instance
    /// </summary>
    public static Window? MainWindow => Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;

    /// <summary>
    /// DI Services
    /// </summary>
    public static IServiceProvider Services { get; private set; } = ServiceConfigurator.ConfigureServices();

    /// <summary>
    /// Logger
    /// </summary>
    private static readonly MyCustomTemplateLogger _log = MyCustomTemplateLogger.For("App");

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (Desktop is { } desktop)
        {
            // Initialize settings
            SettingsService settingsService = Services.GetRequiredService<SettingsService>();
            _ = settingsService.Settings;

            // Initialize Logger
            MyCustomTemplateLogger.Configure(settingsService.Settings.Debug.LogLevel,
                new CompositeLogSink(
                    new ConsoleLogSink(),
                    new FileLogSink(@"Logs\MyCustomTemplate.log")
                )
            );

            // Global exception handlers
            RegisterGlobalExceptionHandlers();

            // Localization
            LocalizationService.LoadLanguage(settingsService.Settings.Ui.Language);

            // Apply saved theme (must happen after Initialize() so FluentAvaloniaTheme is in Styles)
            ThemeService themeService = Services.GetRequiredService<ThemeService>();
            themeService.SetTheme(settingsService.Settings.Ui.Theme);

            // MainWindow
            MainWindow mainWindow = Services.GetRequiredService<MainWindow>();
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Registers global exception handlers for unhandled exceptions
    /// </summary>
    private void RegisterGlobalExceptionHandlers()
    {
        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            args.SetObserved();
            _log.Error("Unobserved task exception occurred");
            _log.LogExceptionDetails(args.Exception);
        };

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            bool isTerminating = args.IsTerminating;
            _log.Error($"Unhandled exception occurred in AppDomain (Terminating: {isTerminating})");
            if (args.ExceptionObject is Exception ex)
            {
                _log.LogExceptionDetails(ex);
            }
            else
            {
                _log.Error($"Non-exception object thrown: {args.ExceptionObject.GetType().FullName ?? "null"}");
            }
        };

        Dispatcher.UIThread.UnhandledException += (_, args) =>
        {
            args.Handled = true;
            _log.Error("Unhandled exception on UI thread");
            _log.LogExceptionDetails(args.Exception);
        };
    }
}