using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
    [AutoMap(typeof(TopicTranslation))]
    public class TopicTranslationDto : SeoBaseDto, ITranslationDto
    {
        [MaxLength(EntityLength.Name)]
        [Required]
        public string Name { get; set; }
        [MaxLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        [MaxLength(EntityLength.LanguageCode)]
        public string Language { get; set; }
    }
}