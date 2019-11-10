using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(ProductTranslation))]
    public class ProductTranslationDto : SeoBaseDto
    {
        public string Name { get; set; }
        [MaxLength(EntityLength.LanguageCode)]
        public string Language { get; set; }
    }
}