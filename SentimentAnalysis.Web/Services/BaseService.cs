namespace SentimentAnalysis.Web.Services
{
    using Microsoft.Extensions.Caching.Memory;
    using SentimentAnalysis.Web.Interfaces;
    using System;
    public class BaseService
    {
        private readonly IMemoryCache _cache;
        private readonly IAppInsightsLoggerService _logger;
        private MemoryCacheEntryOptions cacheOptions;
        public BaseService(IMemoryCache cache, IAppInsightsLoggerService logger)
        {
            _cache = cache;
            _logger = logger;

            this.cacheOptions = new MemoryCacheEntryOptions()
           .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            if (_cache == null)
            {
                throw new ArgumentNullException("Caching service is null");
            }
        }
    }
}
