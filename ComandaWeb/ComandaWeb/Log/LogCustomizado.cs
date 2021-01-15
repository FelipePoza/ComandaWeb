using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComandaWeb.Log
{
    public class LogCustomizado : ILogger
    {
        readonly string _nomeLog;
        readonly ConfiguracaoProvedorLog _configuracaoLog;
        readonly IConfiguration _configuracao;

        public LogCustomizado(string nome,ConfiguracaoProvedorLog configuracaoLog, IConfiguration configuracao)
        {
            _nomeLog = nome;
            _configuracaoLog = configuracaoLog;
            _configuracao = configuracao;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _configuracaoLog.LevelLog;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()} : {eventId.Id} - {formatter(state, exception)}";
            EscreverTextoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivoLog = _configuracao.GetSection("CaminhoLog").GetSection("LogPadrao").Value;
            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog,true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
