using System;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.GUI.ViewModels;
using MyCustomTemplate.GUI.ViewModels.Pages;
using MyCustomTemplate.GUI.Views;
using MyCustomTemplate.Settings;

namespace MyCustomTemplate.GUI.Services;

/// <summary>
/// Provides centralized service configuration and registration for the application,
/// managing dependency injection container setup and service lifecycle management.
/// </summary>
public abstract class ServiceConfigurator
{
    /// <summary>
    /// Configures and registers all application services with the dependency injection container.
    /// </summary>
    /// <returns>An IServiceProvider instance with all configured services.</returns>
    public static IServiceProvider ConfigureServices()
    {
        ServiceCollection services = new ServiceCollection();

        // Settings
        services.AddSingleton<SettingsService>();

        // Services
        services.AddSingleton<IMessageBoxService, MessageBoxService>();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddSingleton<ThemeService>();
        services.AddSingleton<NavigationService>();

        // ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<CardsTestPageViewModel>();

        // Views
        services.AddSingleton<MainWindow>();

        return services.BuildServiceProvider();
    }
}