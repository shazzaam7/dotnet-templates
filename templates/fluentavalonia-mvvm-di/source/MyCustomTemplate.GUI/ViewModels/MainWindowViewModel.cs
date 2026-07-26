using CommunityToolkit.Mvvm.ComponentModel;
using MyCustomTemplate.GUI.Services;

namespace MyCustomTemplate.GUI.ViewModels;

/// <summary>
/// View model for the main application window
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    // Properties
    /// <summary>
    /// The title displayed in the main window title bar
    /// </summary>
    public string WindowTitle { get; } = LocalizationService.GetText("MainWindow.Title");
}