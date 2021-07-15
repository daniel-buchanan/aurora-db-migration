using System.Threading.Tasks;

namespace aurora_db_migration.Common.Setup
{
    public class SetupMigration : BaseMigration
    {
        public override string Name => "setup";

        protected override async Task RunInternal()
        {
            await RunSql("");
        }
    }
}