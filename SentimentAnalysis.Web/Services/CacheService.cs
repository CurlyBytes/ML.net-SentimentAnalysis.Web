using Microsoft.Extensions.Caching.Memory;
using SentimentAnalysis.Web.Interfaces;
using System;

namespace SentimentAnalysis.Web.Services
{
    public class CacheService : ICacheService
    {
        private readonly IAppInsightsLoggerService _logger;
        private readonly IMemoryCache _cache;
        private MemoryCacheEntryOptions cacheOptions;
        public CacheService(IAppInsightsLoggerService logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;

            this.cacheOptions = new MemoryCacheEntryOptions()
           .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }

        public object GetItem(object key)
        {
            try
            {
                object returnVaule;

                _cache.TryGetValue(key, out returnVaule);

                if(returnVaule != null)
                {
                    return returnVaule;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }

        public void SetItem(object key, object value)
        {
            try
            {
                _cache.Set(key, value);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
        }
    }
}
