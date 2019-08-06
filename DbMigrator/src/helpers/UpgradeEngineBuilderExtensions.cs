using System;

using DbUp.Builder;
using DbUp.Helpers;
using Microsoft.Extensions.Logging;

namespace DbMigrator.src.helpers
{
    internal static class UpgradeEngineBuilderExtensions
    {
        public static UpgradeEngineBuilder JournalToggle(this UpgradeEngineBuilder builder, bool toggleNullJournal = true, string table = null)
        {
            if (toggleNullJournal)
                builder.JournalTo(new NullJournal());
            else
            {
                if (String.IsNullOrWhiteSpace(table))
                    throw new ArgumentNullException(nameof(table));
                builder
                    .JournalToSqlTable("dbo", table);
            }
            return builder;
        }

        public static UpgradeEngineBuilder LogTo(this UpgradeEngineBuilder builder, ILogger logger)
        {
            builder.LogTo(new LoggerWrapper(logger));
            return builder;
        }
    }
}
