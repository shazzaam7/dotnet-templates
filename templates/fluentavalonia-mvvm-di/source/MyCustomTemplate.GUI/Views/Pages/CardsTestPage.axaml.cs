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

    /// <summary>
    /// Initializes a new instance of the <see cref="CardsTestPage"/> class.
    /// Resolves <see cref="CardsTestPageViewModel"/> from the DI container and assigns it as the data context.
    /// </summary>
    public CardsTestPage()
    {
        InitializeComponent();

        _viewModel = App.Services.GetRequiredService<CardsTestPageViewModel>();
        DataContext = _viewModel;
    }
}