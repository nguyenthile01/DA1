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
    [AutoMap(typeof(Topic))]
    public class CreateOrEditTopicDto : EntityDto, IHasTranlationDto<TopicTranslationDto>, ISeoEntity
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


        [Display(Name = "Tác giả")]
        public string Author { get; set; }

        [Display(Name = "Đăng bởi")]
        public long? CreatorId { get; set; }


        [Display(Name = "Ảnh đại diện")]
        public int AvatarId { get; set; }

        [Display(Name = "Ảnh banner")]
        public int BannerId { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Images { get; set; }

        [Display(Name = "File tài liệu")]
        public string Document { get; set; }

        [Display(Name = "Loại danh mục")]
        public string CategoryType { get; set; }

        [Display(Name = "Danh mục")]
        public int? CategoryId { get; set; }

        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public virtual List<TopicTranslationDto> Translations { get; set; } = new List<TopicTranslationDto>();

    }
}
