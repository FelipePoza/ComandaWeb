using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ComandaWeb.Log
{
    public class ProvedorLog : ILoggerProvider
    {
        readonly ConfiguracaoProvedorLog _logConfiguracao;
        readonly ConcurrentDictionary<string, LogCustomizado> log = new ConcurrentDictionary<string, LogCustomizado>();
        readonly IConfiguration _configuracao;

        public ProvedorLog(ConfiguracaoProvedorLog logConfiguracao, IConfiguration configuracao)
        {
            _logConfiguracao = logConfiguracao;
            _configuracao = configuracao;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return log.GetOrAdd(categoryName, name => new LogCustomizado(name, _logConfiguracao, _configuracao));
        }

        public void Dispose()
        {
            log.Clear();
        }
    }
}
