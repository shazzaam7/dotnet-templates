using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.ViewModels;

namespace MyCustomTemplate.Views;

public partial class MainWindow : Window
{
    // Properties
    private MainWindowViewModel _viewModel { get; set; }

    // Constructor
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        DataContext = _viewModel;
    }
}