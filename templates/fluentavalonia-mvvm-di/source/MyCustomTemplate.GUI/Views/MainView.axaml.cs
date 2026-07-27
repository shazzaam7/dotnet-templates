using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.GUI.Services;

namespace MyCustomTemplate.GUI.Views;

/// <summary>
/// Shell view containing the NavigationView and content frame for page navigation.
/// </summary>
public partial class MainView : UserControl
{
    private NavigationService _navigationService { get; set; }

    public MainView()
    {
        InitializeComponent();

        // Resolve and wire up the navigation service with the UI controls
        _navigationService = App.Services.GetRequiredService<NavigationService>();
        _navigationService.SetContentFrame(ContentFrame);
        _navigationService.SetNavigationView(NavigationView);

        // Open the default page on startup
        _ = _navigationService.NavigateToTag("CardsTest");
    }

    /// <summary>
    /// Handles menu item clicks by delegating to the navigation service.
    /// </summary>
    private async void NavigationView_OnItemInvoked(object? sender, FANavigationViewItemInvokedEventArgs e)
    {
        if (e.InvokedItemContainer is FANavigationViewItem selectedItem)
        {
            await _navigationService.Navigate(selectedItem, ContentFrame);
        }
    }
}