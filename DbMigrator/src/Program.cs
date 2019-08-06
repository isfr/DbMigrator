using System;
using System.IO;
using System.Linq;
using System.Reflection;

using DbMigrator.src.consts;
using DbMigrator.src.core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

using Env = DbUpTemplate.src.env.EnvironmentEnum;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DbUpTemplate.src
{
    internal class Program
    {
        private static ILogger _logger;

        private static int Main(string[] args)
        {
            if (!Env.Contains(args[0]))
            {
                Console.WriteLine($"First argument must be the environment : {Env.Development} | {Env.Testing} | {Env.Staging} | {Env.Production}");
                return 1;
            }

            var config = Configuration(args[0]);

            ILogger logger = _logger = new LoggerFactory().AddSerilog(ConfigureLogger(config)).CreateLogger("DbMigrator");

            _logger.LogInformation("Configuration loaded and logger created.");

            if (CheckArguments(args) != 0)
            {
                return 1;
            }

            try
            {
                var migrator = new Migrator(logger, config.GetConnectionString("Storage"), config.GetSection("ScriptLocation").GetSection("Path").Value);
                return migrator.Run(args[1]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 1;
            }
        }

        private static IConfiguration Configuration(string environment)
        {
            if (String.IsNullOrWhiteSpace(environment))
                throw new ArgumentNullException(nameof(environment));

            Environment.SetEnvironmentVariable("APP_PATH", Directory.GetCurrentDirectory());

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json")
                .AddEnvironmentVariables()
                .Build();
        }

        private static Logger ConfigureLogger(IConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        private static int CheckArguments(string[] arguments)
        {
            if (arguments == null)
                return 1;

            if (arguments.Length != 2)
            {
                _logger.LogError($"Wrong number of parameters: {arguments.Length} expected: { ParametersConsts.Arguments.EXPECTED_NUMBER_ARGUMENTS }");
                return 1;
            }

            if (!typeof(ParametersConsts.Arguments)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Any(mem => mem.GetRawConstantValue().Equals(arguments[1])))
            {
                _logger.LogError($"The argument {arguments[1]} is not valid. Expected arguments: {ParametersConsts.Arguments.PRE_DEPLOY_ONLY} " +
                    $"| {ParametersConsts.Arguments.MIGRATIONS_ONLY} " +
                    $"| {ParametersConsts.Arguments.POST_DEPLOY_ONLY} " +
                    $"| {ParametersConsts.Arguments.ALL_OPERATIONS}");
                return 1;
            }

            return 0;
        }

    }
}
