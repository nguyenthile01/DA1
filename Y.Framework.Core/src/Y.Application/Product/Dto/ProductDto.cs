using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    //[AutoMap(typeof(Product))]
    public class ProductDto : SeoBaseDto, IEntityDto<int>
    {
        public decimal Price { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}