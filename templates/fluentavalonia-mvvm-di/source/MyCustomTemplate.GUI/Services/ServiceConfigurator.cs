using System;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.GUI.ViewModels;
using MyCustomTemplate.GUI.Views;
using MyCustomTemplate.Settings;

namespace MyCustomTemplate.GUI.Services;

/// <summary>
/// Provides centralized service configuration and registration for the Altair application,
/// managing dependency injection container setup and service lifecycle management.
/// </summary>
public abstract class ServiceConfigurator
{
    /// <summary>
    /// Configures and registers all application services with the dependency injection container.
    /// This method sets up the service collection with all required services and returns
    /// a built service provider ready for use.
    /// </summary>
    /// <returns>An IServiceProvider instance with all configured services.</returns>
    public static IServiceProvider ConfigureServices()
    {
        ServiceCollection services = new ServiceCollection();

        // Settings
        services.AddSingleton<SettingsService>();

        // MessageBox Service
        services.AddSingleton<IMessageBoxService, MessageBoxService>();

        // Notification Service
        services.AddSingleton<INotificationService, NotificationService>();

        // ThemeService
        services.AddSingleton<ThemeService>();

        // Views/ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();

        return services.BuildServiceProvider();
    }
}