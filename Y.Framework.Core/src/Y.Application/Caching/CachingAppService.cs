using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using Y.Configuration.Dto;

namespace Y.Configuration
{
    [AbpAuthorize]
    public class CachingAppService : YAppServiceBase
    {
        private readonly ICacheManager cacheManager;
        public CachingAppService(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        [HttpGet]
        public async Task ClearAllCaches()
        {
            var caches = cacheManager.GetAllCaches();
            foreach (var cache in caches)
            {
                await cache.ClearAsync();
            }
        }
    }
}
