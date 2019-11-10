using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Y.Core
{
    public class SeoBaseEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey, TUser>, ISeoEntity where TUser : IEntity<long>
    {
        [MaxLength(500)]
        public string SeoSlug { get; set; }
        [MaxLength(150)]
        public string MetaTitle { get; set; }
        [MaxLength(500)]
        public string MetaDescription { get; set; }
        [MaxLength(500)]
        public string CanonicalUrl { get; set; }
    }
}
