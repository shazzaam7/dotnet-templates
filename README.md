# .NET Custom Templates

[![Build Templates Package](https://github.com/Shazzaam7/dotnet-templates/actions/workflows/build-templates.yml/badge.svg)](https://github.com/Shazzaam7/dotnet-templates/actions/workflows/build-templates.yml)

Customized .NET templates for rapidly scaffolding applications with consistent architecture and best practices.

## Available Templates

### FluentAvalonia MVVM + DI

A production-ready FluentAvalonia desktop application template with MVVM pattern, dependency injection, custom logging, and settings persistence.

**Template ID:** `fluentavalonia-mvvm-di`

#### Features

- **FluentAvalonia UI 3.0** - Cross-platform desktop UI framework
- **MVVM Pattern** - Using CommunityToolkit.Mvvm for reactive view models
- **Dependency Injection** - Microsoft.Extensions.DependencyInjection for loose coupling
- **Custom Logging** - Structured logging with file and console targets via separate Logging project
- **Settings Persistence** - JSON-based settings with backup recovery and lenient deserialization
- **Theme System** - Light/Dark/System themes with custom resource dictionary overrides
- **Localization** - Multi-language support with untranslated-string fallback
- **Clean Architecture** - Separated into GUI, Core, Logging, and Settings projects
- **Unit Testing** - NUnit test project pre-configured
- **Scripts** - Development utility scripts for localization, changelog generation, and translation progress
- **GitHub Workflows** - Pre-configured CI/CD workflows with automated releases
- **Issue Templates** - Pre-configured bug report, feature request, and help templates
- **.NET 10** - Targets the latest .NET framework
- **Nullable References** - Enabled for better code safety

#### Project Structure

```
fluentavalonia-mvvm-di/
├── source/
│   ├── MyCustomTemplate.Core/        # Shared models, enums, utilities
│   ├── MyCustomTemplate.GUI/         # Main Avalonia application
│   │   ├── Controls/                 # Custom controls (splash screen, etc.)
│   │   ├── Resources/                # Theme dictionaries, language files
│   │   ├── Services/                 # Theme, localization, message box, notification services
│   │   ├── ViewModels/               # MVVM ViewModels
│   │   └── Views/                    # AXAML views
│   ├── MyCustomTemplate.Logging/     # Custom logging infrastructure
│   └── MyCustomTemplate.Settings/    # JSON-based settings persistence
├── tests/
│   └── MyCustomTemplate.Tests/       # Unit tests (NUnit)
├── scripts/                          # Development scripts
└── .github/                          # GitHub workflows & issue templates
```

#### Technologies Used

| Package | Version | Purpose |
|---------|---------|---------|
| FluentAvaloniaUI | 3.0.2 | Fluent design system on Avalonia |
| Avalonia | 12.1.0 | Cross-platform UI framework |
| Avalonia.Desktop | 12.1.0 | Desktop platform support |
| Avalonia.Fonts.Inter | 12.1.0 | Inter font family |
| Avalonia.Themes.Fluent | 12.1.0 | Fluent theme |
| AvaloniaUI.DiagnosticsSupport | 2.2.3 | Debug diagnostics (Debug only) |
| CommunityToolkit.Mvvm | 8.4.2 | MVVM helpers & source generators |
| Microsoft.Extensions.DependencyInjection | 10.0.10 | Dependency injection container |
| NUnit | 4.3.2 | Unit testing framework |

### FluentAvalonia MVVM + DI + NLog

A production-ready FluentAvalonia desktop application template with MVVM pattern, dependency injection, and NLog-based logging.

**Template ID:** `fluentavalonia-mvvm-di-nlog`

#### Features

- **FluentAvalonia UI 3.0** - Cross-platform desktop UI framework
- **MVVM Pattern** - Using CommunityToolkit.Mvvm for reactive view models
- **Dependency Injection** - Microsoft.Extensions.DependencyInjection for loose coupling
- **NLog Logging** - Structured logging with NLog (configurable, optional)
- **Localization** - Multi-language support with untranslated-string fallback
- **Clean Architecture** - Separated into UI and Core projects
- **Unit Testing** - NUnit test project pre-configured
- **Scripts** - Development utility scripts for localization, changelog generation, translation progress, and translation charts
- **GitHub Workflows** - Pre-configured CI/CD workflows with automated releases
- **Issue Templates** - Pre-configured bug report, feature request, and help templates
- **.NET 10** - Targets the latest .NET framework
- **Nullable References** - Enabled for better code safety

#### Project Structure

```
fluentavalonia-mvvm-di-nlog/
├── source/
│   ├── MyCustomTemplate/             # Main application
│   └── MyCustomTemplate.Core/        # Core business logic (packages, logging config)
├── tests/
│   └── MyCustomTemplate.Tests/       # Unit tests (NUnit)
├── scripts/                          # Development scripts
└── .github/                          # GitHub workflows & issue templates
```

#### Technologies Used

| Package | Version | Purpose |
|---------|---------|---------|
| FluentAvaloniaUI | 3.0.1 | Fluent design system on Avalonia |
| Avalonia | 12.1.0 | Cross-platform UI framework |
| Avalonia.Desktop | 12.1.0 | Desktop platform support |
| Avalonia.Fonts.Inter | 12.1.0 | Inter font family |
| Avalonia.Themes.Fluent | 12.1.0 | Fluent theme |
| AvaloniaUI.DiagnosticsSupport | 2.2.3 | Debug diagnostics |
| CommunityToolkit.Mvvm | 8.4.2 | MVVM helpers & source generators |
| Microsoft.Extensions.DependencyInjection | 10.0.9 | Dependency injection container |
| NLog | 6.1.4 | Structured logging framework |
| NUnit | 4.6.1 | Unit testing framework |

## Installation

### Install from NuGet Package (Recommended)

```bash
dotnet new install Shazzaam.DotNetTemplates
```

### Install from Local Directory

```bash
# Install the FluentAvalonia template
dotnet new install templates/fluentavalonia-mvvm-di

# Install the FluentAvalonia NLog template
dotnet new install templates/fluentavalonia-mvvm-di-nlog
```

## Usage

### Create a New FluentAvalonia Project

```bash
dotnet new fluentavalonia-mvvm-di -n MyNewApp -o ./MyNewApp
```

### Create a New FluentAvalonia NLog Project

```bash
dotnet new fluentavalonia-mvvm-di-nlog -n MyNewApp -o ./MyNewApp
```

### Template Parameters

| Parameter | Description | Default |
|-----------|-------------|---------|
| `-n`, `--name` | Project name | `MyCustomTemplate` |
| `-o`, `--output` | Output directory | Current directory |

### Example

```bash
# Create a new FluentAvalonia app with DI and logging
dotnet new fluentavalonia-mvvm-di -n MyApp -o ./src/MyApp

# Navigate and build
cd ./src/MyApp
dotnet build
dotnet run
```

### Scripts

The templates include scripts in the `scripts/` folder. Some require updates after project creation:

#### fluentavalonia-mvvm-di

| Script | File | Update Required |
|--------|------|-----------------|
| Check localization | `scripts/check_localization.py` | Update project folder path if different from `MyCustomTemplate` |
| Generate changelog | `scripts/generate_changelog.py` | Update `REPOSITORY` to `username/repo` |
| Generate translation progress | `scripts/generate_translation_progress.py` | Update project folder paths |
| Sync localization | `scripts/sync_localization.py` | No changes needed |

#### fluentavalonia-mvvm-di-nlog

| Script | File | Update Required |
|--------|------|-----------------|
| Check localization | `scripts/check_localization.py` | Update project folder path if different from `MyCustomTemplate` |
| Generate changelog | `scripts/generate_changelog.py` | Update `REPOSITORY` to `username/repo` |
| Generate translation progress | `scripts/generate_translation_progress.py` | Update project folder paths |
| Generate translation chart | `scripts/generate_translation_chart.js` | No changes needed |
| Sync localization | `scripts/sync_localization.py` | No changes needed |

### GitHub Workflows

Both templates include GitHub Actions workflows in `.github/workflows/`:

| Workflow | Trigger | Description |
|----------|---------|-------------|
| `build_release.yml` | Push to main/dev, PR closed | Builds the app, runs localization check, creates releases |
| `build_job.yml` | Called by build_release | Builds and tests the app |
| `create_release.yml` | Called by build_release | Creates releases (nightly auto-updates, stable creates draft) |
| `update_translation_progress.yml` | Called by build_release | Updates translation progress chart, creates PR to main |

The workflows release to the same repository:
- **Nightly** - Updates the `nightly` tag automatically
- **Stable** - Creates a draft release (manually publish from GitHub)

Review and update as needed for your project:
- Update project names in workflow files if different from `MyCustomTemplate`
- Update paths in `build_release.yml` if your source/tests directories differ

### Issue Templates

Both templates include issue templates in `.github/ISSUE_TEMPLATE/`. Review and update as needed for your project.

## Development

### Building the Templates

```bash
# FluentAvalonia template
cd templates/fluentavalonia-mvvm-di
dotnet build

# FluentAvalonia NLog template
cd templates/fluentavalonia-mvvm-di-nlog
dotnet build
```

### Running Tests

```bash
# FluentAvalonia template
cd templates/fluentavalonia-mvvm-di
dotnet test

# FluentAvalonia NLog template
cd templates/fluentavalonia-mvvm-di-nlog
dotnet test
```

### Uninstalling the Templates

```bash
# Uninstall FluentAvalonia template
dotnet new uninstall fluentavalonia-mvvm-di

# Uninstall FluentAvalonia NLog template
dotnet new uninstall fluentavalonia-mvvm-di-nlog
```

## Architecture

### Dependency Injection

Services are configured in `ServiceConfigurator.cs`:

```csharp
public static IServiceProvider ConfigureServices()
{
    ServiceCollection services = new ServiceCollection();
    
    // Register services
    // Register views/viewmodels
    services.AddSingleton<MainWindowViewModel>();
    services.AddSingleton<MainWindow>();
    
    return services.BuildServiceProvider();
}
```

### Logging

**fluentavalonia-mvvm-di** provides custom logging through a separate `MyCustomTemplate.Logging` project:

- Trace, Debug, Info, Warning, Error, Critical levels
- Exception detail logging with system information
- Colored console output and file logging
- Global exception handling

**fluentavalonia-mvvm-di-nlog** uses NLog for structured logging:

- Configurable via `nlog.config`
- Console and file targets
- Per-category loggers

### Localization

Language resources are stored in `Resources/Language/` as `.axaml` files, loaded via `LocalizationService`.

## License

See [LICENSE](LICENSE) for details.
