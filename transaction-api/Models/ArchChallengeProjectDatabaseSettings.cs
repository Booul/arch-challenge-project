using TransactionApi.Models.Interfaces;

namespace TransactionApi.Models
{
    public class ArchChallengeProjectDatabaseSettings: IMongoDbDatabaseSettings
    {
        public string ConnectionString { get; set; } = String.Empty;
        public string CollectionName { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}