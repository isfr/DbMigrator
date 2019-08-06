using DbUp.Engine.Output;
using Microsoft.Extensions.Logging;

namespace DbMigrator.src.helpers
{
    /// <summary>
    /// Class to wrap any <see cref="ILogger"/> into an <see cref="IUpgradeLog"/>.
    /// </summary>
    /// <seealso cref="IUpgradeLog" />
    internal class LoggerWrapper : IUpgradeLog
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerWrapper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LoggerWrapper(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Writes the error.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void WriteError(string format, params object[] args)
        {
            _logger.LogError(format, args);
        }

        /// <summary>
        /// Writes the information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void WriteInformation(string format, params object[] args)
        {
            _logger.LogInformation(format, args);
        }

        /// <summary>
        /// Writes the warning.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void WriteWarning(string format, params object[] args)
        {
            _logger.LogWarning(format, args);
        }
    }
}
