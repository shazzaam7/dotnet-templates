# .NET Custom Templates

Customized .NET templates for rapidly scaffolding applications with consistent architecture and best practices.

## Available Templates

### FluentAvalonia MVVM + DI + NLog

A production-ready FluentAvalonia desktop application template with MVVM pattern, dependency injection, and structured logging.

**Template ID:** `fluentavalonia-mvvm-di-nlog`

#### Features

- **FluentAvalonia UI 3.0** - Cross-platform desktop UI framework
- **MVVM Pattern** - Using CommunityToolkit.Mvvm for reactive view models
- **Dependency Injection** - Microsoft.Extensions.DependencyInjection for loose coupling
- **NLog Logging** - Structured logging with file and console targets
- **Clean Architecture** - Separated into UI and Core projects
- **Unit Testing** - NUnit test project pre-configured
- **Exception Handling** - Global exception handlers across all threads
- **Localization Support** - Built-in language resource structure
- **Scripts** - Development utility scripts for localization, translation progress, and changelog generation
- **GitHub Workflows** - Pre-configured CI/CD workflows with automated releases
- **Issue Templates** - Pre-configured bug report and feature request templates
- **.NET 10** - Targets the latest .NET framework
- **Nullable References** - Enabled for better code safety

#### Project Structure

```
fluentavalonia-mvvm-di-nlog/
├── source/
│   ├── MyCustomTemplate/           # UI Layer (FluentAvalonia)
│   │   ├── Views/                  # Avalonia views
│   │   ├── ViewModels/             # MVVM view models
│   │   ├── Services/               # Service configuration & DI
│   │   ├── Resources/              # Localization & assets
│   │   ├── App.axaml(.cs)          # Application entry & config
│   │   └── Program.cs              # Application bootstrap
│   └── MyCustomTemplate.Core/      # Core Layer (Business Logic)
│       └── (Logging, models, etc.)
├── tests/
│   └── MyCustomTemplate.Tests/     # Unit tests (NUnit)
├── scripts/                    # Development scripts
└── .github/                 # GitHub workflows
```

#### Technologies Used

| Package | Version | Purpose |
|---------|---------|---------|
| FluentAvalonia | 3.0.0-preview4 | Cross-platform UI framework |
| Avalonia | 12.0.4 | UI framework |
| Avalonia.Desktop | 12.0.4 | Desktop platform support |
| Avalonia.Fonts.Inter | 12.0.4 | Inter font family |
| Avalonia.Themes.Fluent | 12.0.4 | Fluent theme |
| CommunityToolkit.Mvvm | 8.4.2 | MVVM helpers & source generators |
| Microsoft.Extensions.DependencyInjection | 10.0.8 | Dependency injection container |
| NLog | 6.1.3 | Structured logging |
| NUnit | 4.6.1 | Unit testing framework |

### Avalonia MVVM + DI + NLog

A production-ready Avalonia UI desktop application template with MVVM pattern, dependency injection, and structured logging.

**Template ID:** `avalonia-mvvm-di-nlog`

#### Features

- **Avalonia UI 12.0** - Cross-platform desktop UI framework
- **MVVM Pattern** - Using CommunityToolkit.Mvvm for reactive view models
- **Dependency Injection** - Microsoft.Extensions.DependencyInjection for loose coupling
- **NLog Logging** - Structured logging with file and console targets
- **Clean Architecture** - Separated into UI and Core projects
- **Unit Testing** - NUnit test project pre-configured
- **Exception Handling** - Global exception handlers across all threads
- **Localization Support** - Built-in language resource structure
- **Scripts** - Development utility scripts
- **.NET 10** - Targets the latest .NET framework
- **Nullable References** - Enabled for better code safety

#### Technologies Used

| Package | Version | Purpose |
|---------|---------|---------|
| Avalonia | 12.0.4 | UI framework |
| Avalonia.Desktop | 12.0.4 | Desktop platform support |
| Avalonia.Fonts.Inter | 12.0.4 | Inter font family |
| Avalonia.Themes.Fluent | 12.0.4 | Fluent theme |
| CommunityToolkit.Mvvm | 8.4.2 | MVVM helpers & source generators |
| Microsoft.Extensions.DependencyInjection | 10.0.8 | Dependency injection container |
| NLog | 6.1.3 | Structured logging |
| NUnit | 4.6.1 | Unit testing framework |

### Custom Core Library

A customizable class library template with built-in NLog logging, settings management, and utility helpers for building robust applications.

**Template ID:** `custom-core-library`

#### Features

- **NLog Logging** - Structured logging with console and file targets
- **Settings Management** - JSON-based settings with backup/recovery
- **Path Resolution** - Cross-platform path utilities
- **.NET 10** - Targets the latest .NET framework
- **Nullable References** - Enabled for better code safety
- **Logging Levels** - Dynamic log level control at runtime

#### Project Structure

```
custom-core-library/
└── MyCustomTemplate/
    ├── Converters/                 # JSON converters for serialization
    ├── Logging/                    # Static AppLogger class with NLog
    ├── Settings/                  # Settings model and service
    │   └── Sections/              # Settings sections (debug, etc.)
    ├── Utilities/                # Path resolvers and helpers
    └── MyCustomTemplate.csproj    # Project file
```

#### Technologies Used

| Package | Version | Purpose |
|---------|---------|---------|
| NLog | 6.1.3 | Structured logging |

#### Installation

### Install from Local Directory

```bash
# Install the FluentAvalonia template
dotnet new install templates/fluentavalonia-mvvm-di-nlog

# Install the Avalonia template
dotnet new install templates/avalonia-mvvm-di-nlog

# Install the Custom Core Library template
dotnet new install templates/custom-core-library
```

## Usage

### Create a New FluentAvalonia Project

```bash
dotnet new fluentavalonia-mvvm-di-nlog -n MyNewApp -o ./MyNewApp
```

### Create a New Avalonia Project

```bash
dotnet new avalonia-mvvm-di-nlog -n MyNewApp -o ./MyNewApp
```

### Template Parameters

| Parameter | Description | Default |
|-----------|-------------|---------|
| `-n`, `--name` | Project name | `MyCustomTemplate` |
| `-o`, `--output` | Output directory | Current directory |

### Example

```bash
# Create a new FluentAvalonia app with DI and logging
dotnet new fluentavalonia-mvvm-di-nlog -n MyApp -o ./src/MyApp

# Create a new Avalonia app with DI and logging
dotnet new avalonia-mvvm-di-nlog -n MyApp -o ./src/MyApp

# Navigate and build
cd ./src/MyApp
dotnet build
dotnet run
```

### Create a Custom Core Library

```bash
# Create a new core library
dotnet new custom-core-library -n MyApp.Core -o ./MyApp.Core

# Navigate and build
cd ./MyApp.Core
dotnet build
```

### Scripts

The template includes scripts in the `scripts/` folder. Some require updates after project creation:

| Script | File | Update Required |
|--------|------|-----------------|
| Check localization | `scripts/check_localization.py` | Update project folder path if different from `MyCustomTemplate` (line 17) |
| Generate changelog | `scripts/generate_changelog.py` | Update `REPOSITORY` to `username/repo` (line 30) |
| Generate translation progress | `scripts/generate_translation_progress.py` | Update project folder paths (lines 15, 378, 381) |
| Generate translation chart | `scripts/generate_translation_chart.js` | No changes needed |

### GitHub Workflows

The template includes GitHub Actions workflows in `.github/workflows/`:

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

The template includes issue templates in `.github/ISSUE_TEMPLATE/`. Review and update as needed for your project.

## Development

### Building the Template

```bash
cd templates/avalonia-mvvm-di-nlog
dotnet build
```

### Running Tests

```bash
cd templates/avalonia-mvvm-di-nlog
dotnet test
```

### Uninstalling the Template

```bash
# Uninstall FluentAvalonia template
dotnet new uninstall fluentavalonia-mvvm-di-nlog

# Uninstall Avalonia template
dotnet new uninstall avalonia-mvvm-di-nlog

# Uninstall Custom Core Library template
dotnet new uninstall custom-core-library
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

NLog is integrated through the Core layer with a static `AppLogger` class providing:

- Trace, Debug, Info, Warn, Error, Fatal levels
- Exception detail logging
- Log flushing and shutdown
- Global exception handling

### Localization

Language resources are stored in `Resources/Language/` as `.axaml` files, loaded via `LocalizationService`.

## License

See [LICENSE](LICENSE) for details.
