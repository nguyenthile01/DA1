using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Y.Core;
using Y.Dto;
using Y.Sitemap;
using Y.Sitemap.Dto;

namespace Y.Sitemap.SitemapServices
{
    public class PostSitemap : ISitemapPublishService
    {
        private readonly IRepository<Post> repository;
        private readonly ISitemapAppService sitemapAppService;

        public PostSitemap(IRepository<Post> repository, ISitemapAppService sitemapAppService)
        {
            this.repository = repository;
            this.sitemapAppService = sitemapAppService;
        }

        public List<SitemapNode> GetSitemapNodes(int entityId)
        {
            var results = new List<SitemapNode>();

            var model = repository.GetAll()
                .Include(p => p.Translations)
                .FirstOrDefault(p => p.Id == entityId);

            if (model != null)
            {
                var data = model.MapTo<PostDto>();
                results.Add(new SitemapNode {
                    DisplayName = data.Name,
                    Url = data.DetailUrl
                });

                if (model.CategoryId.HasValue)
                {
                    results.AddRange(sitemapAppService.GetSitemapNodes("postcategory", model.CategoryId.Value));
                }

                return results;
            }

            return results;
        }
    }
}
