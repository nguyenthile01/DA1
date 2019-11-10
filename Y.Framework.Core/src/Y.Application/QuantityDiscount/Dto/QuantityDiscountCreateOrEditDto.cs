using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(QuantityDiscount))]
    public class QuantityDiscountCreateOrEditDto : EntityDto
    {
        public int DiscountPercent { get; set; }
        public int MinQuantity { get; set; }
        public string SelectedEvents { get; set; }
    }
}