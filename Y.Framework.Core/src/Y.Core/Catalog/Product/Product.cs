using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    public class Product : BaseAuditedEntity, IMultiLingualEntity<ProductTranslation>
    {
        public string CategoryType { get; set; }
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public int ViewCount { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool ShowOnMenu { get; set; }
        public bool IsAllowBooking { get; set; }
        public int AvatarId { get; set; }
        public int BannerId { get; set; }
        [MaxLength(256)]
        public string ImageIds { get; set; }
        [MaxLength(1000)]
        public string Medias { get; set; }
        public decimal Price { get; set; }
        public string PriceUnit { get; set; }
        [MaxLength(1000)]
        public string Package { get; set; }
        public string HashTags { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new HashSet<ProductAttribute>();

        public ICollection<ProductTranslation> Translations { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}