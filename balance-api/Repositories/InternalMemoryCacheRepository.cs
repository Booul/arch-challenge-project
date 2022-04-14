using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using BalanceApi.Models.Enums;

namespace BalanceApi.Repositories
{
    public class InternalMemoryCacheRepository : IInternalMemoryCacheRepository
    {
        const int TIME_TO_EXPIRATION = 5;

        private readonly IMemoryCache internalCache;
        private readonly ILogger logger;

        public InternalMemoryCacheRepository(IMemoryCache internalCache, ILogger logger) {
            this.internalCache = internalCache;
            this.logger = logger;
        }

        public string Get(string key)
        {
            logger.WriteLine(LoggerType.Log, $"Get cache of {key}.");
            internalCache.TryGetValue(key, out string value);
            return value;
        }

        public void Remove(string key)
        {
            logger.WriteLine(LoggerType.Log, $"Remove cache of {key}.");
            internalCache.Remove(key);
        }

        public void Set(string key, string value)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(TIME_TO_EXPIRATION);

            logger.WriteLine(LoggerType.Log, $"Set cache of {key} -> {value}.");
            internalCache.Set(key, value, options);
        }
    }
}