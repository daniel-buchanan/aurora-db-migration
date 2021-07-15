using System;
using System.Threading.Tasks;

namespace aurora_db_migration.Common
{
    public abstract class BaseMigration : IMigration
    {
        private ITransaction _transaction;

        protected string TransactionId => _transaction.Id;

        public abstract string Name { get; }

        public async Task<MigrationResult> Run()
        {
            // TODO: check if already run

            await _transaction.Begin();
            
            try
            {
                await RunInternal();
            }
            catch (Exception ex)
            {
                try
                {
                    await _transaction.Rollback();
                    return MigrationResult.RolledBack;
                }
                catch (Exception rollbackEx)
                {
                    return MigrationResult.Unknown;
                }
            }

            try
            {
                await _transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    await _transaction.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    return MigrationResult.Unknown;
                }
            }

            return MapResult(_transaction.Result);
        }

        protected abstract Task RunInternal();

        protected Task RunSql(string sql)
        {
            
        }

        private MigrationResult MapResult(TransactionResult input)
        {
            MigrationResult result = MigrationResult.Unknown;

            switch(input)
            {
                case TransactionResult.Unknown:
                    result = MigrationResult.Unknown;
                    break;
                case TransactionResult.Committed:
                    result = MigrationResult.Committed;
                    break;
                case TransactionResult.Rolledback:
                    result = MigrationResult.RolledBack;
                    break;
            }

            return result;
        }
    }
}