using System;
using Microsoft.Extensions.Logging;

namespace Worker
{
    public class CustomLoggerConsole : ILogger
    {
        private readonly string _categoryName;

        public CustomLoggerConsole(string categoryName)
        {
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = logLevel == LogLevel.Information
            ? ConsoleColor.Blue
            : logLevel == LogLevel.Warning
                ? ConsoleColor.Yellow
                : logLevel == LogLevel.Error
                    ? ConsoleColor.DarkRed
                    : logLevel == LogLevel.Critical
                        ? ConsoleColor.Magenta
                        : logLevel == LogLevel.Debug
                            ? ConsoleColor.Green
                            : defaultColor;
            Console.WriteLine($"[{DateTimeOffset.Now.ToString("o")}] {logLevel}: {_categoryName}[{eventId.Id}]: {formatter(state, exception)}");
            Console.ForegroundColor = defaultColor;
        }
    }
}