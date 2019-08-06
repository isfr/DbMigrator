namespace DbMigrator.src.core
{
    /// <summary>
    /// Interface to be implemented for the command executors.
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        /// Runs the configured command command.
        /// </summary>
        /// <param name="toggleNullJournal">if set to <c>true</c> uses the NullJournal</param>
        /// <returns>The result of the operation, 0 if was successful or 1 if it failed.</returns>
        int RunCommand(bool toggleNullJournal = true);
    }
}
