using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.GUI.Services;

namespace MyCustomTemplate.GUI.ViewModels.Pages;

/// <summary>
/// ViewModel for the <see cref="Views.Pages.CardsTestPage"/>.
/// Provides test bindings for all card controls and a command to show an InfoBar notification.
/// </summary>
public partial class CardsTestPageViewModel : ObservableObject
{
    /// <summary>
    /// Gets or sets the test value bound to the <see cref="Controls.Cards.ToggleSwitchCard"/>.
    /// </summary>
    [ObservableProperty] private bool _toggleValue;

    /// <summary>
    /// Gets or sets the selected index bound to the <see cref="Controls.Cards.ComboBoxCard"/>.
    /// </summary>
    [ObservableProperty] private int _comboIndex;

    /// <summary>
    /// Gets or sets the test text value bound to the <see cref="Controls.Cards.TextBoxCard"/>.
    /// </summary>
    [ObservableProperty] private string? _textValue;

    /// <summary>
    /// Gets or sets the test slider value (0-100) bound to the <see cref="Controls.Cards.SliderCard"/>.
    /// </summary>
    [ObservableProperty] private double _sliderValue = 50;

    /// <summary>
    /// Gets or sets the test numeric value bound to the <see cref="Controls.Cards.NumberBoxCard"/>.
    /// </summary>
    [ObservableProperty] private double _numberValue = 100;

    /// <summary>
    /// Gets the items displayed in the <see cref="Controls.Cards.ComboBoxCard"/> test control.
    /// </summary>
    public List<string> ComboItems { get; } = ["Option A", "Option B", "Option C"];

    /// <summary>
    /// Shows an informational notification in the InfoBar via <see cref="INotificationService"/>.
    /// </summary>
    [RelayCommand]
    private void ShowNotification()
    {
        INotificationService notificationService = App.Services.GetRequiredService<INotificationService>();
        notificationService.ShowInfo("Hello from CardsTestPage! Card controls are working.");
    }
}