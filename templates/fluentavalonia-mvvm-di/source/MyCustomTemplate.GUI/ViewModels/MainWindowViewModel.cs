using CommunityToolkit.Mvvm.ComponentModel;
using MyCustomTemplate.GUI.Services;

namespace MyCustomTemplate.GUI.ViewModels;

/// <summary>
/// ViewModel for the main application window.
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    /// <summary>
    /// Gets the title displayed in the main window title bar, localized via <see cref="LocalizationService"/>.
    /// </summary>
    public string WindowTitle { get; } = LocalizationService.GetText("MainWindow.Title");
}