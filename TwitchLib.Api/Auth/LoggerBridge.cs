using Microsoft.Extensions.Logging;
using Swan.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Auth
{
    internal class LoggerBridge : Swan.Logging.ILogger
    {
        private Microsoft.Extensions.Logging.ILogger _logger;

        public LoggerBridge(Microsoft.Extensions.Logging.ILogger logger) => _logger = logger;

        public Swan.Logging.LogLevel LogLevel => Swan.Logging.LogLevel.Info;

        public void Dispose()
        {
        }

        public void Log(LogMessageReceivedEventArgs logEvent)
        {
            _logger.LogInformation(logEvent.Message);
        }
    }
}
