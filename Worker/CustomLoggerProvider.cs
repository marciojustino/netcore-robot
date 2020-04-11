namespace Worker
{
    using Microsoft.Extensions.Logging;

    public class CustomLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLoggerConsole(categoryName);
        }

        public void Dispose()
        {
            
        }
    }
}