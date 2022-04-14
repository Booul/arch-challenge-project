using MongoDB.Bson.Serialization.Attributes;
using BalanceApi.Models.Interfaces;

namespace BalanceApi.Models.Bases
{
    public class MongoDbEntity : IMongoDbEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}