using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
    public class QuantityDiscountExcelDto
    {
        [DisplayName("Phần trăm giảm giá")]
        public int DiscountPercent { get; set; }
        [DisplayName("Số lượng vé tối thiểu")]
        public int MinQuantity { get; set; }
        [DisplayName("Sự kiện")]
        public string SelectedEvents { get; set; }
    }
}