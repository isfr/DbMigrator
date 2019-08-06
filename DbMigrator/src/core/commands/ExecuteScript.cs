using System;

using DbMigrator.src.exceptions;
using DbMigrator.src.helpers;
using DbUp;
using DbUp.ScriptProviders;
using Microsoft.Extensions.Logging;

namespace DbMigrator.src.core.commands
{
    internal class ExecuteScript : ICommand
    {
        private readonly ILogger _logger;

        private readonly string _connectionStirng;

        private readonly string _scriptLocation;

        private readonly string _currentPhase;

        private readonly string _table;

        public ExecuteScript(ILogger logger, string connectionString, string scriptLocation, string currentPhase, string table = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionStirng = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _scriptLocation = scriptLocation ?? throw new ArgumentNullException(nameof(scriptLocation));
            _currentPhase = currentPhase ?? throw new ArgumentNullException(nameof(currentPhase));
            _table = table;
        }
        public int RunCommand(bool toggleNullJournal = true)
        {
            _logger.LogInformation($"Start executing {_currentPhase} scripts.");
            var ScriptsExecutor =
                DeployChanges.To
                .SqlDatabase(_connectionStirng)
                .WithScriptsFromFileSystem($"{_scriptLocation}\\{_currentPhase}", new FileSystemScriptOptions
                {
                    IncludeSubDirectories = true
                })
                .LogTo(_logger)
                .JournalToggle(toggleNullJournal, _table)
                .Build();

            var upgradeResult = ScriptsExecutor.PerformUpgrade();

            _logger.LogInformation("Script executed, checking result. ");

            if (!upgradeResult.Successful)
            {
                throw new ScriptExecutionException(upgradeResult.Error.ToString());
            }

            _logger.LogInformation($"Finished {_currentPhase} scripts execution.");
            return 0;
        }
    }
}
