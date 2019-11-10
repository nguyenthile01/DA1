using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Y.Core
{
    [NotMapped]
    public class BaseCategory : BaseAuditedEntity, IMultiLingualEntity<CategoryTranslation>,ISeoEntity
    {
        #region Localize

        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        [MaxLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        #region SEO

        [MaxLength(500)]
        public string SeoSlug { get; set; }
        [MaxLength(150)]
        public string MetaTitle { get; set; }
        [MaxLength(500)]
        public string MetaDescription { get; set; }
        [MaxLength(500)]
        public string CanonicalUrl { get; set; }

        #endregion

        #endregion

        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual BaseCategory Parent { get; set; }
        public int AvatarId { get; set; }
        public int BannerId { get; set; }
        [MaxLength(256)]
        public string Images { get; set; }
        public virtual ICollection<BaseCategory> Children { get; set; }
        public virtual ICollection<CategoryTranslation> Translations { get; set; }
    }
}