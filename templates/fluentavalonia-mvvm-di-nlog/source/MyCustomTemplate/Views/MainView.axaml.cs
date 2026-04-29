using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.ViewModels;

namespace MyCustomTemplate.Views;

public partial class MainView : UserControl
{
    // Properties
    private MainViewViewModel _viewModel { get; set; }

    // Constructor
    public MainView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<MainViewViewModel>();
        DataContext = _viewModel;
    }
}