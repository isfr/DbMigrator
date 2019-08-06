namespace DbMigrator.src.consts
{
    internal static class ParametersConsts
    {
        internal static class Arguments
        {
            public const string PRE_DEPLOY_ONLY = "PreDeployment";

            public const string POST_DEPLOY_ONLY = "PostDeployment";

            public const string ALL_OPERATIONS = "AllOperations";

            public const string MIGRATIONS_ONLY = "Migrations";

            public const int EXPECTED_NUMBER_ARGUMENTS = 2;
        }
    }
}
