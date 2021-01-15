using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandaWeb.Log
{
    public class ConfiguracaoProvedorLog
    {
        public LogLevel LevelLog { get; set; } = LogLevel.Warning;
        public int IdEvento { get; set; } = 0;
    }
}
