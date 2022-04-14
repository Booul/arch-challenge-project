using TransactionApi.Models.Interfaces;
using MongoDB.Driver;
using TransactionApi.Models.Bases;
using TransactionApi.Models.Enums;

namespace TransactionApi.Repositories.Abstracts
{
    public abstract class MongoDbRepository<T> : IDbRepository<T> where T: MongoDbEntity
    {
        private readonly IMongoDbDatabaseSettings settings;
        private readonly IMongoCollection<T> collection;
        private readonly ILogger logger;

        public MongoDbRepository(IMongoDbDatabaseSettings settings, ILogger logger) {
            this.settings = settings;

            var mongoClient = new MongoClient(settings.ConnectionString);

            var database = mongoClient.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<T>(settings.CollectionName);

            this.logger = logger;
        }

        public virtual List<T>? List()
        {
            logger.WriteLine(LoggerType.Log, "List transactions.");
            return collection.Find(item => true).ToList();
        }

        public virtual T? Post(T? item)
        {
            if (item == null) return null;
            try
            {
                item.CreatedAt = DateTime.Now.ToUniversalTime();
                collection.InsertOne(item);
                logger.WriteLine(LoggerType.Log, $"Save transaction: {item.ToString()}.");
            }
            catch (System.Exception)
            {
                throw;
            }

            return item;
        }
    }
}