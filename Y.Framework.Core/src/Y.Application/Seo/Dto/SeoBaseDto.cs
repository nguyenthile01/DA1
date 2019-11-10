using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    public class SeoBaseDto : ISeoEntity
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string CanonicalUrl { get; set; }
        public string SeoSlug { get; set; }
    }
}