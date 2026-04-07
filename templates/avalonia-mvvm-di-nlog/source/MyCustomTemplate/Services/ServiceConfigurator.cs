using System;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.Core.Logging;
using MyCustomTemplate.ViewModels;
using MyCustomTemplate.Views;

namespace MyCustomTemplate.Services;

/// <summary>
/// Configures and registers application services, views, and view models with the dependency injection container.
/// Centralizes all service registration to ensure proper dependency resolution throughout the application.
/// </summary>
public static class ServiceConfigurator
{
    /// <summary>
    /// Configures the dependency injection container by registering all application services, views, and view models.
    /// Builds and returns an <see cref="IServiceProvider"/> for resolving registered dependencies.
    /// </summary>
    /// <returns>
    /// An <see cref="IServiceProvider"/> instance that can resolve all registered services, views, and view models.
    /// </returns>
    /// <exception cref="Exception">
    /// Thrown when service registration or container building fails. Details are logged before re-throwing.
    /// </exception>
    public static IServiceProvider ConfigureServices()
    {
        Logger.Debug("Configuring dependency injection services");
        try
        {
            ServiceCollection services = new ServiceCollection();

            // Register Services here

            // Register Views/ViewModels here
            // Pages

            // Views
            // Windows
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to configure services");
            Logger.LogExceptionDetails(ex);
            throw;
        }
    }
}