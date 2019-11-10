using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    [Table("Category")]
    public class Category : BaseAuditedEntity, IMultiLingualEntity<CategoryTranslation>
    {
        public Category()
        {
            Children = new HashSet<Category>();
            Posts = new HashSet<Post>();
        }
        #region Localize

        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        [MaxLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        #region SEO

        [MaxLength(150)]
        public string MetaTitle { get; set; }
        [MaxLength(500)]
        public string MetaDescription { get; set; }
        [MaxLength(500)]
        public string CanonicalUrl { get; set; }

        #endregion

        #endregion

        [ForeignKey("Parent")]
        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }

        [Display(Name = "Loại danh mục")]
        [MaxLength(EntityLength.Address)]
        public string CategoryType { get; set; }


        [Display(Name = "Ảnh đại diện")]
        public int AvatarId { get; set; }

        [Display(Name = "Ảnh banner")]
        public int BannerId { get; set; }

        [Display(Name = "Hình ảnh")]
        [MaxLength(EntityLength.Name)]
        public string Images { get; set; }
      
        
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual ICollection<CategoryTranslation> Translations { get; set; }
    }
}
