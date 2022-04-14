using BalanceApi.Models.Interfaces;
using BalanceApi.Repositories.Abstracts;
using BalanceApi.Models;

namespace BalanceApi.Repositories
{
    public class BalanceRepository : MongoDbRepository<Transaction>
    {
        public BalanceRepository(
            IMongoDbDatabaseSettings settings, BalanceApi.Repositories.ILogger logger
        ): base(settings, logger){}
    }
}