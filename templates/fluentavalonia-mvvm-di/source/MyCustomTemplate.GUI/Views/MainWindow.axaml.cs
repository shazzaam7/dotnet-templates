using FluentAvalonia.UI.Windowing;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.GUI.Controls;
using MyCustomTemplate.GUI.ViewModels;

namespace MyCustomTemplate.GUI.Views;

/// <summary>
/// The main application window
/// </summary>
public partial class MainWindow : FAAppWindow
{
    private MainWindowViewModel _viewModel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        DataContext = _viewModel;
        SplashScreen = new AppSplashScreen();
    }
}