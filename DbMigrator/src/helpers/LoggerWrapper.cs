using System;
using System.Collections.Generic;
using System.Text;
using DbUp.Engine.Output;
using Microsoft.Extensions.Logging;

namespace DbMigrator.src.helpers
{
    internal class LoggerWrapper : IUpgradeLog
    {
        private readonly ILogger _logger;

        public LoggerWrapper(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string format, params object[] args)
        {
            _logger.LogError(format, args);
        }

        public void WriteInformation(string format, params object[] args)
        {
            _logger.LogInformation(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            _logger.LogWarning(format, args);
        }
    }
}
