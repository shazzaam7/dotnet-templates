using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.GUI.ViewModels.Pages;

namespace MyCustomTemplate.GUI.Views.Pages;

/// <summary>
/// Test page showcasing all card controls with an InfoBar notification button.
/// </summary>
public partial class CardsTestPage : UserControl
{
    private CardsTestPageViewModel _viewModel { get; set; }

    public CardsTestPage()
    {
        InitializeComponent();

        // Resolve the ViewModel from DI and assign it as the data context
        _viewModel = App.Services.GetRequiredService<CardsTestPageViewModel>();
        DataContext = _viewModel;
    }
}