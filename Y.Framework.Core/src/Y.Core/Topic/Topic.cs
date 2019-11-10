using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    public class Topic : BaseAuditedEntity, IMultiLingualEntity<TopicTranslation>
    {
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

        [Display(Name = "Hiển thị trên menu")]
        public bool ShowInMenu { get; set; }


        [Display(Name = "Cho phép bình luận")]
        public Nullable<bool> AllowComments { get; set; }

        [Display(Name = "Số lượt xem")]
        public Nullable<long> ViewCount { get; set; }

        [Display(Name = "Tác giả")]
        [MaxLength(EntityLength.Name)]
        public string Author { get; set; }

        [Display(Name = "Đăng bởi")]
        public long? CreatorId { get; set; }

        [Display(Name = "Creator")]
        [ForeignKey(nameof(CreatorId))]
        public virtual User Creator { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public int AvatarId { get; set; }

        [Display(Name = "Ảnh banner")]
        public int BannerId { get; set; }

        [Display(Name = "Hình ảnh")]
        [MaxLength((EntityLength.Url))]
        public string Images { get; set; }

        [MaxLength((EntityLength.Name))]
        [Display(Name = "File tài liệu")]
        public string Document { get; set; }

        [Display(Name = "Loại danh mục")]
        [MaxLength(EntityLength.Phone)]
        public string CategoryType { get; set; }

        [Display(Name = "Danh mục")]
        public int? TopicCategoryId { get; set; }

        [ForeignKey(nameof(TopicCategoryId))]
        public virtual TopicCategory TopicCategory { get; set; }


        public virtual ICollection<TopicTranslation> Translations { get; set; }
    }
}
