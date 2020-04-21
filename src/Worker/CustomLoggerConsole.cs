namespace Worker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Microsoft.Extensions.Logging;

    public class CustomLoggerConsole : ILogger
    {
        private readonly string _categoryName;
        public Dictionary<int, ConsoleColor> _threadsRunning { get; set; }
        private static object _MessageLock = new object();

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
            // var color = Console.ForegroundColor;
            // var threadsColors = _threadsRunning.Where(t => t.Key.Equals(Thread.CurrentThread.ManagedThreadId));
            // if (!threadsColors.Any())
            // {
            //     _threadsRunning.Add(Thread.CurrentThread.ManagedThreadId, ConsoleColor.)
            // }

            lock (_MessageLock)
            {
                if (!IsEnabled(logLevel))
                {
                    return;
                }

                Console.WriteLine();
                Console.Write("[");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{DateTimeOffset.Now.ToString("o")}");

                Console.ResetColor();
                Console.Write("]");

                Console.ForegroundColor = logLevel == LogLevel.Information
                ? ConsoleColor.Green
                : logLevel == LogLevel.Warning
                    ? ConsoleColor.Yellow
                    : logLevel == LogLevel.Error
                        ? ConsoleColor.DarkRed
                        : logLevel == LogLevel.Critical
                            ? ConsoleColor.Magenta
                            : ConsoleColor.Blue;

                Console.Write($" {logLevel}");

                Console.ResetColor();
                Console.Write($": {_categoryName}[");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{eventId.Id}");

                Console.ResetColor();
                Console.Write($"]: {formatter(state, exception)}");
            }
        }
    }
}