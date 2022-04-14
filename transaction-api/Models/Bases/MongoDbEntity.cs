using MongoDB.Bson.Serialization.Attributes;
using TransactionApi.Models.Interfaces;

namespace TransactionApi.Models.Bases
{
    public class MongoDbEntity : IMongoDbEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}