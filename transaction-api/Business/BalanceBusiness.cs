using TransactionApi.Models.Enums;
using TransactionApi.Repositories;
using TransactionApi.Repositories.Abstracts;
using TransactionApi.Business.Interfaces;
using TransactionApi.Data.Converter.Contract;
using TransactionApi.Data.VO;
using TransactionApi.Models;

namespace TransactionApi.Business
{
   
    public class BalanceBusiness : IBalanceBusiness<Transaction, TransactionVO>
    {
        private readonly IParser<TransactionVO, Transaction> transactionVOConverter;
        private readonly MongoDbRepository<Transaction> balanceRepository;
        private readonly ICache cache;

        public BalanceBusiness(
            IParser<TransactionVO, Transaction> transactionVOConverter,
            MongoDbRepository<Transaction> balanceRepository,
            ICache cache
        )
        {
            this.transactionVOConverter = transactionVOConverter;
            this.balanceRepository = balanceRepository;
            this.cache = cache;
        }

        public void RebuildBalance()
        {
            List<Transaction>? transactions = balanceRepository.List();
            if (transactions == null || transactions?.Count == 0)
                return;
            
            List<string> transactionUidsAccount = GetAllTransactionUidsAccount(transactions);
            DeleteAllKeys(transactionUidsAccount);

            transactions.ForEach(transactions => {
                SaveBalance(transactions);
            });
        }

        public Transaction Save(TransactionVO data)
        {
            if (data.TypeId < 0 || data.TypeId > 1) throw new Exception("Invalid transaction type.");
            Transaction? transaction = balanceRepository.Post(transactionVOConverter.Parse(data));

            if (transaction == null) throw new Exception("Could not store transaction.");
            else {
                SaveBalance(transaction);
                return transaction;
            }
        }

        private void SaveBalance (Transaction transaction) {
            try
            {
                string currentBalance = cache.Get(transaction.UidAccount.ToString());
                double currentBalanceConverted = Convert.ToDouble(currentBalance);
                double newBalance = CalculateBalance(currentBalanceConverted, transaction);

                cache.Set(transaction.UidAccount.ToString(), newBalance.ToString());
            }
            catch (System.Exception)
            { 
                throw;
            }
        }

        private double CalculateBalance (double currentBalance, Transaction currentTransaction) {
            if (currentTransaction.Type == TransactionType.Credit) {
                return currentBalance + MathF.Abs((float)currentTransaction.Value);
            } else if (currentTransaction.Type == TransactionType.Debit) {
                return currentBalance - MathF.Abs((float)currentTransaction.Value);
            } else {
                throw new Exception("Invalid transaction type.");
            }
        }

        private void DeleteAllKeys (List<string> keys) {
            foreach (var key in keys)
                cache.Remove(key);
        }

        private List<string> GetAllTransactionUidsAccount (List<Transaction> transactions) {
            List<string> transactionUidAccounts = new List<string>();

            transactions.ForEach(transaction => {
                if (!transactionUidAccounts.Contains(transaction.UidAccount.ToString()))
                    transactionUidAccounts.Add(transaction.UidAccount.ToString());
            });

            return transactionUidAccounts;
        }
    }
}