using TransactionApi.Models.Bases;
using TransactionApi.Models.Enums;
using TransactionApi.Models.Interfaces;

namespace TransactionApi.Models
{
    public class Transaction: MongoDbEntity, IBalance
    {
        public Guid UidAccount { get; set; }
        public TransactionType Type { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"UidAccount: {UidAccount}, Type: {Type.ToString()}, Value: {Value}, Date: {Date}";
        }
    }
}