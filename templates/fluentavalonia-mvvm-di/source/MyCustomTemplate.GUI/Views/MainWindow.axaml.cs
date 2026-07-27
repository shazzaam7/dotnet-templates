using FluentAvalonia.UI.Windowing;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.GUI.Controls;
using MyCustomTemplate.GUI.ViewModels;

namespace MyCustomTemplate.GUI.Views;

/// <summary>
/// The main application window, hosting <see cref="MainView"/> as its content.
/// </summary>
public partial class MainWindow : FAAppWindow
{
    private MainWindowViewModel _viewModel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// Resolves <see cref="MainWindowViewModel"/> from the DI container and assigns the splash screen.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        DataContext = _viewModel;
        SplashScreen = new AppSplashScreen();
    }
}