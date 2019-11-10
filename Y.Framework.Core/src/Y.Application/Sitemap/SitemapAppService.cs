//using Abp.Dependency;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Y.Core;
//using Y.Sitemap.Dto;
//using Y.Sitemap.SitemapServices;

//namespace Y.Sitemap
//{
//    public class SitemapAppService : ISitemapAppService
//    {
//        private readonly IIocResolver _iocResolver;

//        public SitemapAppService(IIocResolver iocResolver)
//        {
//            _iocResolver = iocResolver;
//        }

//        private Type GetSitemapPublishType(string entityName)
//        {
//            Type type = null;

//            switch (entityName)
//            {
//                case "event": type = typeof(EventSitemap); break;
//                case "post": type = typeof(PostSitemap); break;
//                case "postcategory": type = typeof(PostCategorySitemap); break;
//            }

//            return type;
//        }

//        public List<SitemapNode> GetSitemapNodes(string entityName, int entityId)
//        {
//            Type sitemapType = GetSitemapPublishType(entityName);

//            if (sitemapType == null)
//                return new List<SitemapNode>();

//            using (var sitemap = _iocResolver.ResolveAsDisposable<ISitemapPublishService>(sitemapType))
//            {
//                var results = sitemap.Object.GetSitemapNodes(entityId);
//                return results;
//            }
//        }

//        public List<SitemapNode> GetSitemapTree(string entityName, int entityId)
//        {
//            var results = GetSitemapNodes(entityName, entityId);

//            if (results.HasData())
//            {
//                results.Reverse();
//                return results;
//            }

//            return new List<SitemapNode>();
//        }
//    }
//}
