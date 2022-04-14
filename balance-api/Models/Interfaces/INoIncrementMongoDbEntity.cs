using MongoDB.Bson.Serialization.Attributes;

namespace BalanceApi.Models.Interfaces
{
    public interface IMongoDbEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
    }
}