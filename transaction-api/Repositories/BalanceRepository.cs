using TransactionApi.Models.Interfaces;
using TransactionApi.Repositories.Abstracts;
using TransactionApi.Models;

namespace TransactionApi.Repositories
{
    public class BalanceRepository : MongoDbRepository<Transaction>
    {
        public BalanceRepository(
            IMongoDbDatabaseSettings settings, TransactionApi.Repositories.ILogger logger
        ): base(settings, logger){}
    }
}