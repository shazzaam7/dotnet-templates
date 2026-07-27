using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAvalonia.UI.Controls;
using MyCustomTemplate.GUI.Views.Pages;

namespace MyCustomTemplate.GUI.Services;

/// <summary>
/// Handles navigation between pages in the NavigationView content frame.
/// </summary>
public class NavigationService
{
    private FAFrame? _contentFrame;
    private FANavigationView? _navigationView;
    private string? _currentPageTag;

    /// <summary>
    /// Tag of the page currently displayed in the content frame.
    /// </summary>
    public string? CurrentPageTag => _currentPageTag;

    /// <summary>
    /// Raised after navigation completes with the new page tag.
    /// </summary>
    public event EventHandler<string>? Navigated;

    /// <summary>
    /// Registers the content frame where pages are hosted.
    /// </summary>
    public void SetContentFrame(FAFrame frame) => _contentFrame = frame;

    /// <summary>
    /// Registers the navigation view used to update selection state.
    /// </summary>
    public void SetNavigationView(FANavigationView navigationView) => _navigationView = navigationView;

    /// <summary>
    /// Navigates to the page associated with the invoked navigation item.
    /// </summary>
    public async Task Navigate(FANavigationViewItem item, FAFrame? contentFrame = null)
    {
        string tag = item.Tag?.ToString() ?? string.Empty;
        await NavigateToTag(tag, contentFrame);
    }

    /// <summary>
    /// Navigates to the page matching the given tag string.
    /// </summary>
    public async Task NavigateToTag(string tag, FAFrame? contentFrame = null)
    {
        FAFrame? frame = contentFrame ?? _contentFrame;
        _currentPageTag = tag;

        switch (tag)
        {
            case "CardsTest":
                frame?.Navigate(typeof(CardsTestPage));
                break;
        }

        UpdateSelection(tag);
        Navigated?.Invoke(this, tag);
    }

    /// <summary>
    /// Sets IsSelected on the matching NavigationViewItem to keep the UI in sync.
    /// </summary>
    private void UpdateSelection(string tag)
    {
        if (_navigationView == null) return;
        foreach (object? item in _navigationView.MenuItems ?? Array.Empty<object>())
        {
            if (item is FANavigationViewItem navItem)
            {
                navItem.IsSelected = navItem.Tag?.ToString() == tag;
            }
        }
    }
}