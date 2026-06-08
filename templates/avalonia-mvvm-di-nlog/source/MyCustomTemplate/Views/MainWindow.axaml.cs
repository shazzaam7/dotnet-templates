using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.ViewModels;

namespace MyCustomTemplate.Views;

/// <summary>
/// The main application window
/// </summary>
public partial class MainWindow : Window
{
    // Fields
    /// <summary>
    /// The view model bound to this window
    /// </summary>
    private MainWindowViewModel _viewModel { get; set; }

    // Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        DataContext = _viewModel;
    }
}