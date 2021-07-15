using System.Threading.Tasks;

namespace aurora_db_migration.Common
{
    public interface IMigration
    {
        string Name { get; }
        Task<MigrationResult> Run();
    }
}