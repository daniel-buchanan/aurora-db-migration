using System.Threading.Tasks;

namespace aurora_db_migration.Common
{
    public interface ITransaction
    {
        string Id { get; }
        Task Begin();
        Task Commit();
        Task Rollback();
        TransactionResult Result { get; }
    }
}