using System;

namespace Y.Dto
{
    public class QuantityDiscountFilterDto : YPagedAndSortDto
    {
        public int? DiscountPercent { get; set; }
        public int? MinQuantity { get; set; }
    }
}