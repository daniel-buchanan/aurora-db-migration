using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.RDSDataService;
using Amazon.RDSDataService.Model;

namespace aurora_db_migration.Common
{
    public class Transaction : ITransaction
    {
        private IAmazonRDSDataService _service;
        public string Id { get; protected set; }

        public TransactionResult Result { get; protected set; }

        public async Task Begin()
        {
            var x = await _service.BeginTransactionAsync(new BeginTransactionRequest() {

            });

            Id = x.TransactionId;
        }

        public async Task Commit()
        {
            var x = await _service.CommitTransactionAsync(new CommitTransactionRequest() {
                TransactionId = Id
            });

            if(x.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new Exception();
            }

            Result = TransactionResult.Committed;
        }

        public async Task Rollback()
        {
            var x = await _service.RollbackTransactionAsync(new RollbackTransactionRequest() {
                TransactionId = Id
            });

            if(x.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new Exception();
            }

            Result = TransactionResult.Rolledback;
        }
    }
}