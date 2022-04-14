using BalanceApi.Repositories;
using BalanceApi.Repositories.Abstracts;
using BalanceApi.Business.Interfaces;
using BalanceApi.Models;

namespace BalanceApi.Business
{
   
    public class BalanceBusiness : IBalanceBusiness
    {
        private readonly IRedisCacheBusiness redisBalanceBusiness;
        private readonly MongoDbRepository<Transaction> balanceRepository;
        private readonly IInternalMemoryCacheRepository internalMemoryCache;

        public BalanceBusiness(
            IRedisCacheBusiness redisBalanceBusiness,
            MongoDbRepository<Transaction> balanceRepository,
            IInternalMemoryCacheRepository internalMemoryCache
        )
        {
            this.redisBalanceBusiness = redisBalanceBusiness;
            this.balanceRepository = balanceRepository;
            this.internalMemoryCache = internalMemoryCache;
        }

        public double Get(string uidAccount)
        {
            try
            {
                double? memoryCacheValue = GetInternalMemoryCache(uidAccount);
                if (memoryCacheValue != null)
                    return (double)memoryCacheValue;

                double redisCacheValue = GetRedisCache(uidAccount);
                internalMemoryCache.Set(uidAccount, redisCacheValue.ToString());
                return redisCacheValue;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private double? GetInternalMemoryCache (string uidAccount) {
            try
            {
                string result = internalMemoryCache.Get(uidAccount);

                if (!String.IsNullOrEmpty(result))
                    return Convert.ToDouble(result);
                else
                    return null;
            }
            catch (System.Exception)
            {        
                throw;
            }
        }

        private double GetRedisCache (string uidAccount) {
            try
            {
                return redisBalanceBusiness.GetBalance(uidAccount);
            }
            catch (System.Exception)
            {   
                throw;
            }
        }
    }
}