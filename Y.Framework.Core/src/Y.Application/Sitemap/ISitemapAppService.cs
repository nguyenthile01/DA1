using Abp.Application.Services;
using System.Collections.Generic;
using Y.Sitemap.Dto;

namespace Y.Sitemap
{
    public interface ISitemapAppService : IApplicationService
    {
        List<SitemapNode> GetSitemapNodes(string entityName, int entityId);
    }
}
