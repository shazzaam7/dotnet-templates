using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MyCustomTemplate.GUI.Services;

namespace MyCustomTemplate.GUI.ViewModels.Pages;

/// <summary>
/// ViewModel for the CardsTestPage. Provides test bindings for all card controls
/// and a command to show an InfoBar notification.
/// </summary>
public partial class CardsTestPageViewModel : ObservableObject
{
    /// <summary>
    /// Test value for ToggleSwitchCard.
    /// </summary>
    [ObservableProperty] private bool _toggleValue;

    /// <summary>
    /// Test value for ComboBoxCard.
    /// </summary>
    [ObservableProperty] private int _comboIndex;

    /// <summary>
    /// Test value for TextBoxCard.
    /// </summary>
    [ObservableProperty] private string? _textValue;

    /// <summary>
    /// Test value for SliderCard.
    /// </summary>
    [ObservableProperty] private double _sliderValue = 50;

    /// <summary>
    /// Test value for NumberBoxCard.
    /// </summary>
    [ObservableProperty] private double _numberValue = 100;

    /// <summary>
    /// Items for ComboBoxCard test.
    /// </summary>
    public List<string> ComboItems { get; } = ["Option A", "Option B", "Option C"];

    /// <summary>
    /// Shows an informational notification in the InfoBar.
    /// </summary>
    [RelayCommand]
    private void ShowNotification()
    {
        INotificationService notificationService = App.Services.GetRequiredService<INotificationService>();
        notificationService.ShowInfo("Hello from CardsTestPage! Card controls are working.");
    }
}