using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.Core.Logging;
using MyCustomTemplate.Core.Settings;
using MyCustomTemplate.Services;
using MyCustomTemplate.Views;

namespace MyCustomTemplate;

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
    public static IServiceProvider Services = ServiceConfigurator.ConfigureServices();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (Desktop != null)
        {
            // Global exception handlers
            Logger.Trace("Registering global exception handlers");
            RegisterGlobalExceptionHandlers();

            // Initialize loading and saving of Settings
            SettingsService settingsService = Services.GetRequiredService<SettingsService>();
            Logger.SetLogLevel(settingsService.Settings.Debug.LogLevel);
            settingsService.SaveSettings();

            // Load Language
            LocalizationService.LoadLanguage();

            // Get MainWindow
            Logger.Debug("Resolving MainWindow from services");
            MainWindow mainWindow = Services.GetRequiredService<MainWindow>();
            Desktop.MainWindow = mainWindow;

            // Wire up window events
            mainWindow.Opened += (_, _) =>
            {
                Logger.Info("Launching Xenia Manager");
                Logger.Debug("Main window opened");
            };

            // Application exit handler
            Desktop.Exit += (_, _) =>
            {
                Logger.Info("Closing Xenia Manager");
                Logger.Debug("Flushing logs before shutdown");
                Logger.Flush();
                Logger.Debug("Shutting down logger");
                Logger.Shutdown();
            };
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
            Logger.Error("Unobserved task exception occurred");
            HandleFatalException(args.Exception);
        };

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            bool isTerminating = args.IsTerminating;
            Logger.Error($"Unhandled exception in AppDomain (Terminating: {isTerminating})");

            if (args.ExceptionObject is Exception ex)
            {
                HandleFatalException(ex);
            }
            else
            {
                Logger.Error($"Non-exception object thrown: {args.ExceptionObject?.GetType().FullName ?? "null"}");
            }
        };

        Dispatcher.UIThread.UnhandledException += (_, args) =>
        {
            args.Handled = true;
            Logger.Error("Unhandled exception on UI thread");
            HandleFatalException(args.Exception);
        };
    }

    private static void HandleFatalException(Exception ex)
    {
        try
        {
            Logger.Error("=== Fatal Exception Encountered ===");
            Logger.LogExceptionDetails(ex, includeEnvironmentInfo: true);

            // Ensure logs are written before a potential crash
            Logger.Flush();
        }
        catch
        {
            // If logging fails, we can do little about it
            // Just ensure we don't throw from the exception handler
        }
    }
}