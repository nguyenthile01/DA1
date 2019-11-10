using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(Post))]
    public class PostDto : ISeoEntity
    {
        public int Id { get; set; }
        #region Localize

        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        [MaxLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        #region SEO

        public string SeoSlug { get; set; }

        [MaxLength(150)]
        public string MetaTitle { get; set; }
        [MaxLength(500)]
        public string MetaDescription { get; set; }
        [MaxLength(500)]
        public string CanonicalUrl { get; set; }
        public string MetaImage
        {
            get
            {
                return this.AvatarUrl;
            }
        }
        #endregion

        #endregion
        public int AvatarId { get; set; }
        public int BannerId { get; set; }

        [PictureUrl(nameof(AvatarId), 600)]
        public string AvatarUrl { get; set; }
        [PictureUrl(nameof(BannerId), 1920)]
        public string BannerUrl { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public string DetailUrl => $"{SeoSlug}-p{Id}";
    }
}
