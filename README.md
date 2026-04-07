# .NET Custom Templates

Customized .NET templates for rapidly scaffolding applications with consistent architecture and best practices.

## Available Templates

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
- **.NET 10** - Targets the latest .NET framework
- **Nullable References** - Enabled for better code safety

#### Project Structure

```
avalonia-mvvm-di-nlog/
├── source/
│   ├── MyCustomTemplate/           # UI Layer (Avalonia)
│   │   ├── Views/                  # Avalonia views
│   │   ├── ViewModels/             # MVVM view models
│   │   ├── Services/               # Service configuration & DI
│   │   ├── Resources/              # Localization & assets
│   │   ├── App.axaml(.cs)          # Application entry & config
│   │   └── Program.cs              # Application bootstrap
│   └── MyCustomTemplate.Core/      # Core Layer (Business Logic)
│       └── (Logging, models, etc.)
└── tests/
    └── MyCustomTemplate.Tests/     # Unit tests (NUnit)
```

#### Technologies Used

| Package | Version | Purpose |
|---------|---------|---------|
| Avalonia | 12.0.0 | Cross-platform UI framework |
| Avalonia.Desktop | 12.0.0 | Desktop platform support |
| Avalonia.Fonts.Inter | 12.0.0 | Inter font family |
| CommunityToolkit.Mvvm | 8.4.2 | MVVM helpers & source generators |
| Microsoft.Extensions.DependencyInjection | 10.0.5 | Dependency injection container |
| NLog | 6.1.2 | Structured logging |
| NUnit | 4.5.1 | Unit testing framework |

## Installation

### Install from Local Directory

```bash
# Navigate to the template directory
cd templates/avalonia-mvvm-di-nlog

# Install the template
dotnet new install .
```

## Usage

### Create a New Project

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
# Create a new Avalonia app with DI and logging
dotnet new avalonia-mvvm-di-nlog -n MyApp -o ./src/MyApp

# Navigate and build
cd ./src/MyApp
dotnet build
dotnet run
```

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
dotnet new uninstall avalonia-mvvm-di-nlog
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

NLog is integrated through the Core layer with a static `Logger` class providing:

- Trace, Debug, Info, Warn, Error, Fatal levels
- Exception detail logging
- Log flushing and shutdown
- Global exception handling

### Localization

Language resources are stored in `Resources/Language/` as `.axaml` files, loaded via `LocalizationService`.

## License

See [LICENSE](LICENSE) for details.
