using Abp.Application.Services;
using System.Collections.Generic;
using Y.Sitemap.Dto;

namespace Y.Sitemap
{
    public interface ISitemapPublishService : IApplicationService
    {
        List<SitemapNode> GetSitemapNodes(int entityId);
    }
}
