using System;

using DbUp.Builder;
using DbUp.Helpers;
using Microsoft.Extensions.Logging;

namespace DbMigrator.src.helpers
{
    /// <summary>
    /// Extension method to encapsulatesome logic and add more functionality to the builder class.
    /// </summary>
    internal static class UpgradeEngineBuilderExtensions
    {
        /// <summary>
        /// Method to change the journal between NullJournal and SqlTable journal.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="toggleNullJournal">if set to <c>true</c> configures the NullJournal.</param>
        /// <param name="table">The database table to save the journal.</param>
        /// <returns>The builder itself.</returns>
        /// <exception cref="ArgumentNullException">The database table to save the </exception>
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

        /// <summary>
        /// Override the LogsTo to inject an any logger based on <see cref="ILogger"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="logger">The logger to be used.</param>
        /// <returns>The builder itself.</returns>
        public static UpgradeEngineBuilder LogTo(this UpgradeEngineBuilder builder, ILogger logger)
        {
            builder.LogTo(new LoggerWrapper(logger));
            return builder;
        }
    }
}
