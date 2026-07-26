using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace MyCustomTemplate.Logging;

/// <summary>
/// The central logging orchestrator and per-category logger for the application.
/// Provides static configuration methods and factory-level logger creation via <see cref="For"/>.
/// </summary>
/// <remarks>
/// <para>
/// This class serves two roles: a static orchestrator that manages the global logging sink and
/// minimum level, and a per-category logger instance that captures caller context automatically.
/// </para>
/// <para>
/// At application startup, call <see cref="Configure"/> to set up the log sink (e.g., a
/// <see cref="CompositeLogSink"/> with <see cref="ConsoleLogSink"/> and <see cref="FileLogSink"/>).
/// </para>
/// <para>
/// Throughout the application, obtain a logger via <see cref="For"/> and call level-specific
/// methods. Caller file, line, and member name are captured automatically via compiler attributes.
/// </para>
/// <code>
/// // At startup:
/// MyCustomTemplateLogger.Configure(sink: new CompositeLogSink(
///     new ConsoleLogSink(),
///     new FileLogSink("app.log")));
///
/// // In consumer code:
/// private static readonly MyCustomTemplateLogger _log = MyCustomTemplateLogger.For("MyService");
/// _log.Info("Service started");
/// </code>
/// </remarks>
public sealed class MyCustomTemplateLogger
{
    // --- Static orchestrator ---
    private static readonly ConcurrentDictionary<string, MyCustomTemplateLogger> Loggers = new ConcurrentDictionary<string, MyCustomTemplateLogger>(StringComparer.Ordinal);

    private static readonly Lock Sync = new Lock();
    private static volatile LogLevel _minimumLevel = LogLevel.Info;
    private static ILogSink _sink = new ConsoleLogSink(useColors: true);

    /// <summary>
    /// Gets or sets the minimum <see cref="LogLevel"/> below which log entries are silently dropped.
    /// Defaults to <see cref="LogLevel.Info"/>.
    /// </summary>
    /// <remarks>
    /// Setting this to <see cref="LogLevel.None"/> disables all logging, including file output.
    /// This property is thread-safe for reads via volatile semantics.
    /// </remarks>
    public static LogLevel MinimumLevel
    {
        get => _minimumLevel;
        set => _minimumLevel = value;
    }

    /// <summary>
    /// Gets or sets the active <see cref="ILogSink"/> that receives all log entries.
    /// </summary>
    /// <remarks>
    /// Replacing the sink automatically disposes the previous sink
    /// if it implements <see cref="IDisposable"/>.
    /// All access is synchronized via <see cref="Sync"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when setting to <c>null</c>.
    /// </exception>
    public static ILogSink Sink
    {
        get
        {
            lock (Sync)
            {
                return _sink;
            }
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            lock (Sync)
            {
                if (ReferenceEquals(_sink, value))
                {
                    return;
                }

                if (_sink is IDisposable disposable)
                {
                    try
                    {
                        disposable.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }

                _sink = value;
            }
        }
    }

    /// <summary>
    /// Configures the logging system.
    /// Should be called once at application startup before any log entries are emitted.
    /// </summary>
    /// <param name="minimumLevel">
    /// Optional minimum log level.
    /// If <c>null</c>, the current level is preserved.
    /// </param>
    /// <param name="sink">
    /// Optional log sink.
    /// If <c>null</c>, the current sink is preserved.
    /// </param>
    public static void Configure(LogLevel? minimumLevel = null, ILogSink? sink = null)
    {
        if (minimumLevel.HasValue)
        {
            _minimumLevel = minimumLevel.Value;
        }

        if (sink is not null)
        {
            Sink = sink;
        }
    }

    /// <summary>
    /// Updates the minimum log level at runtime and logs a confirmation message.
    /// </summary>
    /// <param name="level">The new minimum log level to apply.</param>
    public static void SetLogLevel(LogLevel level)
    {
        _minimumLevel = level;
        MyCustomTemplateLogger logger = For("App");
        logger.Info($"Logging level updated: {level}");
    }

    /// <summary>
    /// Disposes the active sink if it implements <see cref="IDisposable"/> and flushes
    /// any buffered log entries to disk. Should be called during application shutdown.
    /// </summary>
    /// <remarks>
    /// After calling this method, logging continues to work for non-disposable sinks
    /// (e.g., <see cref="ConsoleLogSink"/>), but file sinks will no longer write.
    /// </remarks>
    public static void Shutdown()
    {
        lock (Sync)
        {
            if (_sink is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    /// <summary>
    /// Returns a per-category <see cref="MyCustomTemplateLogger"/> instance.
    /// Logger instances are cached by category name, so repeated calls with the same
    /// category return the same instance.
    /// </summary>
    /// <param name="category">
    /// A string identifying the logging category (e.g., "Settings", "DI", "Localization").
    /// Typically, the application or module name is used as the category.
    /// </param>
    /// <returns>
    /// A cached <see cref="MyCustomTemplateLogger"/> for the specified category.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="category"/> is null or whitespace.
    /// </exception>
    public static MyCustomTemplateLogger For(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);
        return Loggers.GetOrAdd(category, static key => new MyCustomTemplateLogger(key));
    }

    /// <summary>
    /// Attempts to parse a string into a <see cref="LogLevel"/>.
    /// Supports standard enum names and common aliases
    /// ("warn" for Warning, "fatal" for Critical).
    /// Parsing is case-insensitive.
    /// </summary>
    /// <param name="text">
    /// The string to parse (e.g., "Info", "warning", "3").
    /// </param>
    /// <param name="level">
    /// When this method returns <c>true</c>, contains the parsed <see cref="LogLevel"/>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the string was successfully parsed; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryParseLevel(string? text, out LogLevel level)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            level = default;
            return false;
        }

        string normalized = text.Trim();
        if (Enum.TryParse<LogLevel>(normalized, ignoreCase: true, out level) && Enum.IsDefined(level))
        {
            return true;
        }

        if (string.Equals(normalized, "warn", StringComparison.OrdinalIgnoreCase))
        {
            level = LogLevel.Warning;
            return true;
        }

        if (string.Equals(normalized, "fatal", StringComparison.OrdinalIgnoreCase))
        {
            level = LogLevel.Critical;
            return true;
        }

        level = default;
        return false;
    }

    /// <summary>
    /// Determines whether a log entry at the specified level would be emitted
    /// given the current <see cref="MinimumLevel"/>.
    /// </summary>
    /// <param name="level">The log level to check.</param>
    /// <returns>
    /// <c>true</c> if entries at this level should be written; otherwise, <c>false</c>.
    /// </returns>
    internal static bool IsEnabled(LogLevel level)
    {
        if (_minimumLevel == LogLevel.None)
        {
            return false;
        }

        return level >= _minimumLevel;
    }

    /// <summary>
    /// Constructs a <see cref="LogEntry"/> and writes it to the active sink
    /// if the level is enabled.
    /// </summary>
    internal static void Write(LogLevel level,
        string category, string message, Exception? exception,
        string sourceFilePath, int sourceLine, string sourceMemberName)
    {
        if (!IsEnabled(level))
        {
            return;
        }

        LogEntry entry = new LogEntry(DateTimeOffset.Now, level, category, message,
            Path.GetFileName(sourceFilePath), sourceLine, sourceMemberName,
            exception);

        ILogSink sink;
        lock (Sync)
        {
            sink = _sink;
        }

        sink.Write(in entry);
    }

    // --- Per-category logger instance ---

    /// <summary>
    /// Initializes a new logger instance for the specified category.
    /// Instances should be obtained via <see cref="For"/> rather than constructed directly.
    /// </summary>
    /// <param name="category">The category name for this logger.</param>
    private MyCustomTemplateLogger(string category)
    {
        Category = category;
    }

    /// <summary>
    /// Gets the category name associated with this logger instance.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Logs a message at <see cref="LogLevel.Trace"/> with automatic caller context capture.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="sourceFilePath">Automatically captured source file path.</param>
    /// <param name="sourceLine">Automatically captured source line number.</param>
    /// <param name="sourceMemberName">Automatically captured caller member name.</param>
    public void Trace(
        string message,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLine = 0,
        [CallerMemberName] string sourceMemberName = "")
        => Write(LogLevel.Trace, Category, message, exception: null, sourceFilePath, sourceLine, sourceMemberName);

    /// <summary>
    /// Logs a message at <see cref="LogLevel.Debug"/> with automatic caller context capture.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="sourceFilePath">Automatically captured source file path.</param>
    /// <param name="sourceLine">Automatically captured source line number.</param>
    /// <param name="sourceMemberName">Automatically captured caller member name.</param>
    public void Debug(
        string message,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLine = 0,
        [CallerMemberName] string sourceMemberName = "")
        => Write(LogLevel.Debug, Category, message, exception: null, sourceFilePath, sourceLine, sourceMemberName);

    /// <summary>
    /// Logs a message at <see cref="LogLevel.Info"/> with automatic caller context capture.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="sourceFilePath">Automatically captured source file path.</param>
    /// <param name="sourceLine">Automatically captured source line number.</param>
    /// <param name="sourceMemberName">Automatically captured caller member name.</param>
    public void Info(
        string message,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLine = 0,
        [CallerMemberName] string sourceMemberName = "")
        => Write(LogLevel.Info, Category, message, exception: null, sourceFilePath, sourceLine, sourceMemberName);

    /// <summary>
    /// Logs a message at <see cref="LogLevel.Warning"/> with automatic caller context capture.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="sourceFilePath">Automatically captured source file path.</param>
    /// <param name="sourceLine">Automatically captured source line number.</param>
    /// <param name="sourceMemberName">Automatically captured caller member name.</param>
    public void Warning(
        string message,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLine = 0,
        [CallerMemberName] string sourceMemberName = "")
        => Write(LogLevel.Warning, Category, message, exception: null, sourceFilePath, sourceLine, sourceMemberName);

    /// <summary>
    /// Logs a message at <see cref="LogLevel.Error"/> with automatic caller context capture.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">An optional exception to include with the log entry.</param>
    /// <param name="sourceFilePath">Automatically captured source file path.</param>
    /// <param name="sourceLine">Automatically captured source line number.</param>
    /// <param name="sourceMemberName">Automatically captured caller member name.</param>
    public void Error(
        string message,
        Exception? exception = null,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLine = 0,
        [CallerMemberName] string sourceMemberName = "")
        => Write(LogLevel.Error, Category, message, exception, sourceFilePath, sourceLine, sourceMemberName);

    /// <summary>
    /// Logs a message at <see cref="LogLevel.Critical"/> with automatic caller context capture.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">An optional exception to include with the log entry.</param>
    /// <param name="sourceFilePath">Automatically captured source file path.</param>
    /// <param name="sourceLine">Automatically captured source line number.</param>
    /// <param name="sourceMemberName">Automatically captured caller member name.</param>
    public void Critical(
        string message,
        Exception? exception = null,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLine = 0,
        [CallerMemberName] string sourceMemberName = "")
        => Write(LogLevel.Critical, Category, message, exception, sourceFilePath, sourceLine, sourceMemberName);

    /// <summary>
    /// Logs a detailed exception report at <see cref="LogLevel.Critical"/> level, including
    /// the full exception chain, stack traces, and optional system environment information.
    /// </summary>
    /// <param name="ex">The exception to log.</param>
    /// <param name="includeEnvironmentInfo">
    /// If <c>true</c>, includes machine name, OS version, .NET runtime, process architecture,
    /// and current directory in the report.
    /// </param>
    /// <param name="memberName">Automatically captured caller member name.</param>
    /// <param name="filePath">Automatically captured caller file path.</param>
    /// <param name="lineNumber">Automatically captured caller line number.</param>
    public void LogExceptionDetails(Exception ex, bool includeEnvironmentInfo = true,
        [CallerMemberName] string? memberName = null, [CallerFilePath] string? filePath = null, [CallerLineNumber] int lineNumber = 0)
    {
        string context = FormatContext(memberName, filePath, lineNumber);
        Critical($"[{context}] ===== Exception Report Start =====");
        Critical($"[{context}] Timestamp (UTC): {DateTime.UtcNow:O}");

        LogExceptionWithDepth(ex, context);

        if (includeEnvironmentInfo)
        {
            Critical($"[{context}] === System Information ===");
            Critical($"[{context}] Machine Name: {Environment.MachineName}");
            Critical($"[{context}] OS Version: {Environment.OSVersion}");
            Critical($"[{context}] .NET Runtime: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}");
            Critical($"[{context}] Process Architecture: {System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture}");
            Critical($"[{context}] Current Directory: {Environment.CurrentDirectory}");
        }

        Critical($"[{context}] ===== Exception Report End =====");
    }

    /// <summary>
    /// Formats caller context into a standardized <c>FileName.MemberName:LineNumber</c> string
    /// for inclusion in log output.
    /// </summary>
    private static string FormatContext(string? memberName, string? filePath, int lineNumber)
    {
        if (string.IsNullOrEmpty(memberName))
        {
            return "Unknown";
        }

        string fileName = filePath != null
            ? Path.GetFileNameWithoutExtension(filePath)
            : "Unknown";

        return lineNumber > 0
            ? $"{fileName}.{memberName}:{lineNumber}"
            : $"{fileName}.{memberName}";
    }

    /// <summary>
    /// Recursively logs each level of an exception chain with indented detail,
    /// including type, message, source, stack trace, data entries, and inner exceptions.
    /// </summary>
    /// <param name="ex">The exception to log.</param>
    /// <param name="context">The caller context prefix for each line.</param>
    /// <param name="depth">The current depth in the inner exception chain.</param>
    private void LogExceptionWithDepth(Exception ex, string? context = null, int depth = 0)
    {
        while (true)
        {
            string indent = new string(' ', depth * 2);
            string prefix = context != null ? $"[{context}] " : "";

            Critical($"{prefix}{indent}Exception Level: {depth}");
            Critical($"{prefix}{indent}Type: {ex.GetType().FullName}");
            Critical($"{prefix}{indent}Message: {ex.Message}");
            Critical($"{prefix}{indent}Source: {ex.Source}");
            Critical($"{prefix}{indent}HResult: {ex.HResult}");
            if (ex.HelpLink != null)
            {
                Critical($"{prefix}{indent}Help Link: {ex.HelpLink}");
            }

            if (ex.Data.Count > 0)
            {
                Critical($"{prefix}{indent}Data:");
                foreach (object? key in ex.Data.Keys)
                {
                    Critical($"{prefix}{indent}  {key}: {ex.Data[key]}");
                }
            }

            if (!string.IsNullOrWhiteSpace(ex.StackTrace))
            {
                Critical($"{prefix}{indent}StackTrace:");
                foreach (string line in ex.StackTrace.Split(Environment.NewLine))
                {
                    Critical($"{prefix}{indent}  {line}");
                }
            }

            if (ex.TargetSite != null)
            {
                Critical($"{prefix}{indent}TargetSite: {ex.TargetSite}");
            }

            if (ex.InnerException != null)
            {
                Critical($"{prefix}{indent}--- Inner Exception ---");
                ex = ex.InnerException;
                depth = depth + 1;
                continue;
            }
            break;
        }
    }
}