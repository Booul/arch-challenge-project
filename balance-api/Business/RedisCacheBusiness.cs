using BalanceApi.Models;
using BalanceApi.Business.Interfaces;
using BalanceApi.Models.Exceptions;
using BalanceApi.Models.Enums;
using BalanceApi.Repositories;
using BalanceApi.Repositories.Abstracts;

namespace BalanceApi.Business
{
    public class RedisCacheBusiness : IRedisCacheBusiness
    {
        private readonly MongoDbRepository<Transaction> balanceRepository;
        private readonly IRedisCacheRepository redisCache;

        public RedisCacheBusiness(
            MongoDbRepository<Transaction> balanceRepository,
            IRedisCacheRepository redisCache
        )
        {
            this.balanceRepository = balanceRepository;
            this.redisCache = redisCache;
        }
        
        public double GetBalance(string uidAccount)
        {
            try
            {
                string result =  redisCache.Get(uidAccount);
                double? redisCacheValue = !String.IsNullOrEmpty(result) ? Convert.ToDouble(result) : null;
                if (redisCacheValue != null) return (double)redisCacheValue;

                double? rebuildCacheValue = RebuildBalance(uidAccount);
                if (rebuildCacheValue != null) return (double)rebuildCacheValue;

                throw new Exception("Unable to recover balance.");
            }
            catch (System.Exception)
            {  
                throw;
            }
        }

        public double? RebuildBalance(string uidAccount)
        {
            List<Transaction>? transactions = balanceRepository.List()?.Where(balance => balance.UidAccount.ToString().Equals(uidAccount)).ToList();
            if (transactions == null || transactions?.Count == 0)
                throw new NoTransactionsException();
            
            redisCache.Remove(uidAccount);

            transactions?.ForEach(transaction => {
                SaveBalance(transaction);
            });
            return Convert.ToDouble(redisCache.Get(uidAccount));
        }

        private void SaveBalance (Transaction transaction) {
            try
            {
                string currentBalance = redisCache.Get(transaction.UidAccount.ToString());
                double currentBalanceConverted = Convert.ToDouble(currentBalance);
                double newBalance = CalculateBalance(currentBalanceConverted, transaction);

                redisCache.Set(transaction.UidAccount.ToString(), newBalance.ToString());
            }
            catch (System.Exception)
            { 
                throw;
            }
        }

        private double CalculateBalance (double currentBalance, Transaction currentTransaction) {
            if (currentTransaction.Type == TransactionType.Credit) {
                return currentBalance + currentTransaction.Value;
            } else if (currentTransaction.Type == TransactionType.Debit) {
                return currentBalance - currentTransaction.Value;
            } else {
                throw new Exception("Invalid transaction type.");
            }
        }
    }
}