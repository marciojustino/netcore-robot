namespace Infrastructure.Logs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Microsoft.Extensions.Logging;

    public class CustomLoggerConsole : ILogger
    {
        private readonly string _categoryName;
        private static Dictionary<int, ConsoleColor> _threadsRunning;
        private static List<ConsoleColor> _colors;
        private static object _MessageLock = new object();

        public CustomLoggerConsole(string categoryName)
        {
            _categoryName = categoryName;
            _threadsRunning = new Dictionary<int, ConsoleColor>();
            _colors = new List<ConsoleColor> {
                ConsoleColor.Blue,
                ConsoleColor.Cyan,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkCyan,
                ConsoleColor.DarkGray,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkYellow,
                ConsoleColor.Gray,
                ConsoleColor.Green,
                ConsoleColor.Magenta,
                ConsoleColor.Red,
                ConsoleColor.White,
                ConsoleColor.Yellow
            };
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

            lock (_MessageLock)
            {
                var backupColor = Console.ForegroundColor;
                var threadsColors = _threadsRunning.Where(t => t.Key.Equals(Thread.CurrentThread.ManagedThreadId));
                if (!threadsColors.Any())
                {
                    var index = new Random().Next(0, _colors.Count);
                    _threadsRunning.Add(Thread.CurrentThread.ManagedThreadId, _colors[index]);
                    Console.ForegroundColor = _colors[index];
                }
                else
                {
                    Console.ForegroundColor = threadsColors.FirstOrDefault().Value;
                }

                Console.WriteLine($"{DateTimeOffset.Now.ToString("o")} | {logLevel.ToString().ToUpper()} | Thread:[{Thread.CurrentThread.ManagedThreadId}] | {_categoryName}[{eventId.Id}]: {formatter(state, exception)}");
                Console.ResetColor();
            }
        }
    }
}