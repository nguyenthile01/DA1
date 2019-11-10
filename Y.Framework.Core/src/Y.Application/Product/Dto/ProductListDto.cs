using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Y.Dto
{
    public class ProductListDto: PagedAndSortedResultRequestDto
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}