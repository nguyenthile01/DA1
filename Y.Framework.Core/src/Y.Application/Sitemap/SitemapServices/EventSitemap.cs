//using Abp.AutoMapper;
//using Abp.Domain.Repositories;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Y.Core;
//using Y.Dto;
//using Y.Sitemap;
//using Y.Sitemap.Dto;

//namespace Y.Sitemap.SitemapServices
//{
//    public class EventSitemap : ISitemapPublishService
//    {
//        private readonly IRepository<Event> repository;

//        public EventSitemap(IRepository<Event> repository)
//        {
//            this.repository = repository;
//        }

//        public List<SitemapNode> GetSitemapNodes(int entityId)
//        {
//            var results = new List<SitemapNode>();

//            var model = repository.GetAll()
//                .Include(p => p.Translations)
//                .FirstOrDefault(p => p.Id == entityId);
//            //
//            return results;
//        }
//    }
//}
