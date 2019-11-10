using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(Product))]
    public class ProductCreateDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public List<ProductTranslationDto> Translations { get; set; }
    }
}