using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Y.Core;
using Y.Dto;
using Y.Sitemap.Dto;

namespace Y.Sitemap.SitemapServices
{
    public class PostCategorySitemap : ISitemapPublishService
    {
        private readonly IRepository<PostCategory> repository;

        public PostCategorySitemap(IRepository<PostCategory> repository)
        {
            this.repository = repository;
        }

        public List<SitemapNode> GetSitemapNodes(int entityId)
        {
            var results = new List<SitemapNode>();

            var model = repository.GetAll()
                .Include(p => p.Translations)
                .FirstOrDefault(p => p.Id == entityId);

            if (model != null)
            {
                var data = model.MapTo<PostCategoryDto>();
                results.Add(new SitemapNode {
                    DisplayName = data.Name,
                    Url = data.DetailUrl
                });
                return GetSitemapNodes(results, model.ParentId);
            }

            return results;
        }

        private List<SitemapNode> GetSitemapNodes(List<SitemapNode> nodes, int? parentId = null)
        {
            if (!parentId.HasValue)
                return nodes;

            var model = repository.GetAll()
                .Include(p => p.Translations)
                .FirstOrDefault(p => p.Id == parentId.Value);

            if (model != null)
            {
                var data = model.MapTo<PostCategoryDto>();
                nodes.Add(new SitemapNode {
                    DisplayName = data.Name,
                    Url = data.DetailUrl
                });

                return GetSitemapNodes(nodes, model.ParentId);
            }

            return nodes;
        }
    }
}
