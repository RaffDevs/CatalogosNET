namespace APICatalogo.App.Logging;

public class CustomLogger : ILogger
{
    private readonly string _loggerName;
    private readonly CustomLoggerProviderConfiguration _loggerConfig;

    public CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
    {
        _loggerName = loggerName;
        _loggerConfig = loggerConfig;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == _loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        WriteLogFile(message);
    }

    public void WriteLogFile(string message)
    {
        string logFilePath = @"c:\dados\log\developer.log.txt";

        using (StreamWriter streamWriter = new StreamWriter(logFilePath, true))
        {
            try
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}