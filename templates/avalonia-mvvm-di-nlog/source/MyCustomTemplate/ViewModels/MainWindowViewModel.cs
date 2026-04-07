using CommunityToolkit.Mvvm.ComponentModel;
using MyCustomTemplate.Services;

namespace MyCustomTemplate.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public string WindowTitle { get; } = LocalizationService.GetText("MainWindow.Title");
}