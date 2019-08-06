using System;

namespace DbMigrator.src.exceptions
{
    [Serializable]
    internal class ScriptExecutionException : Exception
    {
        public ScriptExecutionException(string message)
           : base(message)
        {
        }
    }
}
