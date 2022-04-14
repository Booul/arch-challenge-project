using BalanceApi.Models.Enums;

namespace BalanceApi.Models.Interfaces
{  
    public interface IBalance
    {
        public TransactionType Type { get; set; }
        public double Value { get; set; }
    }
}