using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
    [AutoMap(typeof(TopicCategory))]
    public class CreateOrEditTopicCategoryDto : EntityDto, IHasTranlationDto<TopicCategoryTranslationDto>,ISeoEntity
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
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public virtual List<TopicCategoryTranslationDto> Translations { get; set; } = new List<TopicCategoryTranslationDto>();

    }
}
