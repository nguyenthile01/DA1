using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(Product))]
    public class ProductUpdateDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public List<ProductTranslationDto> Translations { get; set; }
    }
}