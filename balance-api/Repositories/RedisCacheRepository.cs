using Microsoft.Extensions.Caching.Distributed;
using BalanceApi.Models.Enums;

namespace BalanceApi.Repositories
{
    public class RedisCacheRepository : IRedisCacheRepository
    {
        private readonly IDistributedCache distributedCache;
        private readonly ILogger logger;

        public RedisCacheRepository(IDistributedCache distributedCache, ILogger logger) {
            this.distributedCache = distributedCache;
            this.logger = logger;
        }

        public string Get(string key)
        {
            logger.WriteLine(LoggerType.Log, $"Get cache of {key}.");
            return distributedCache.GetString(key);
        }

        public void Remove(string key)
        {
            logger.WriteLine(LoggerType.Log, $"Remove cache of {key}.");
            distributedCache.Remove(key);
        }

        public void Set(string key, string value)
        {
            logger.WriteLine(LoggerType.Log, $"Set cache of {key} -> {value}.");
            distributedCache.SetString(key, value);
        }
    }
}