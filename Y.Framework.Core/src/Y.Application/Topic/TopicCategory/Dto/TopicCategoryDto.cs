using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(TopicCategory))]
    public class TopicCategoryDto : IEntityDto<int>
    {
        public int Id { get; set; }
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

        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }

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
        public DateTime CreationTime { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
