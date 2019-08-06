namespace DbMigrator.src.core
{
    internal interface ICommand
    {
        int RunCommand(bool toggleNullJournal = true);
    }
}
