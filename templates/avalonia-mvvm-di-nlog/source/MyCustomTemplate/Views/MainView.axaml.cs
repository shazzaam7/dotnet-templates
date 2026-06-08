using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.ViewModels;

namespace MyCustomTemplate.Views;

/// <summary>
/// The main application view
/// </summary>
public partial class MainView : UserControl
{
    // Fields
    /// <summary>
    /// The view model bound to this view
    /// </summary>
    private MainViewViewModel _viewModel { get; set; }

    // Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="MainView"/> class
    /// </summary>
    public MainView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<MainViewViewModel>();
        DataContext = _viewModel;
    }
}