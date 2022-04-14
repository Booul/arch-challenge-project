using TransactionApi.Models.Enums;
using TransactionApi.Models.Interfaces;
using TransactionApi.Data.Converter.Contract;
using TransactionApi.Data.VO;
using TransactionApi.Models;

namespace TransactionApi.Data.Converter.Implementations
{
    public class TransactionConverter : IParser<TransactionVO, Transaction>, IParser<Transaction, TransactionVO>
    {
        private readonly UidConverter uidConverter;

        public TransactionConverter()
        {
            this.uidConverter = new UidConverter();
        }

        public Transaction? Parse(TransactionVO origin)
        {
            try
            {
                if (origin == null) return null;
                return new Transaction
                {
                    UidAccount = uidConverter.Parse(origin.UidAccount),
                    Type = (TransactionType)origin.TypeId,
                    Value = origin.Value,
                    Date = origin.Date
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public TransactionVO? Parse(Transaction origin)
        {
            try
            {
                if (origin == null) return null;
                return new TransactionVO
                {
                    UidAccount = uidConverter.Parse((Guid)origin.UidAccount),
                    TypeId = (short)origin.Type,
                    Value = origin.Value,
                    Date = origin.Date
                };
            }
            catch (System.Exception)
            {
                throw;
            }   
        }
    }
}