namespace BalanceApi.Models.Interfaces
{
    public interface IMongoDbDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}