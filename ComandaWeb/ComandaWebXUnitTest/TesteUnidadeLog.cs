using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComandaWebXUnitTest
{
    public static class TesteUnidadeLog
    {
        public static ILogger<T> Create<T>()
        {
            var logger = new XUnitLogger<T>();
            return logger;
        }

        class XUnitLogger<T> : ILogger<T>, IDisposable
        {
            private readonly Action<string> saida = Console.WriteLine;

            public void Dispose()
            {
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
                Func<TState, Exception, string> formatter) => saida(formatter(state, exception));

            public bool IsEnabled(LogLevel logLevel) => true;

            public IDisposable BeginScope<TState>(TState state) => this;
        }
    }
}
