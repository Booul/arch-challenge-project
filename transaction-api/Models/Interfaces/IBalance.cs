using TransactionApi.Models.Enums;

namespace TransactionApi.Models.Interfaces
{  
    public interface IBalance
    {
        public TransactionType Type { get; set; }
        public double Value { get; set; }
    }
}