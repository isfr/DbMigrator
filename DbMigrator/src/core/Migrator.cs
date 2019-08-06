using System;
using System.Collections.Generic;

using DbMigrator.src.consts;
using DbMigrator.src.core.commands;
using Microsoft.Extensions.Logging;

namespace DbMigrator.src.core
{
    internal class Migrator
    {
        private readonly ILogger _logger;

        private readonly string _connectionString;

        private readonly string _scriptLocation;

        public Migrator(ILogger logger, string connectionStirng, string scriptLocation)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionString = connectionStirng ?? throw new ArgumentNullException(nameof(connectionStirng));
            _scriptLocation = scriptLocation ?? throw new ArgumentNullException(nameof(scriptLocation));
        }

        public int Run(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentNullException(nameof(command));

            if (this.Operations.ContainsKey(command))
            {
                _logger.LogInformation($"Launched operation with command: {command}");
                return this.Operations[command].Invoke();
            }
            
            _logger.LogError($"The command: {command} is not configured to be executed.");
            return 1;
        }

        private IDictionary<string, Func<int>> Operations => new Dictionary<string, Func<int>>
        {
            { ParametersConsts.Arguments.PRE_DEPLOY_ONLY, () => new ExecuteScript(_logger, _connectionString, _scriptLocation, ParametersConsts.Arguments.PRE_DEPLOY_ONLY).RunCommand() },
            { ParametersConsts.Arguments.POST_DEPLOY_ONLY, () => new ExecuteScript(_logger, _connectionString, _scriptLocation, ParametersConsts.Arguments.POST_DEPLOY_ONLY).RunCommand() },
            { ParametersConsts.Arguments.MIGRATIONS_ONLY, () => new ExecuteScript(_logger, _connectionString, _scriptLocation, ParametersConsts.Arguments.MIGRATIONS_ONLY, "MigrationsJournal").RunCommand(false) },
            { ParametersConsts.Arguments.ALL_OPERATIONS, () => this.RunAllOperations(new List<Func<int>>{
                Operations[ParametersConsts.Arguments.PRE_DEPLOY_ONLY],
                Operations[ParametersConsts.Arguments.MIGRATIONS_ONLY],
                Operations[ParametersConsts.Arguments.POST_DEPLOY_ONLY]
            }) }
        };

        private int RunAllOperations(IList<Func<int>> myCommands)
        {
            foreach (var command in myCommands)
            {
                command.Invoke();
            }
            return 0;
        }
    }
}
