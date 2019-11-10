using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    [AutoMapFrom(typeof(QuantityDiscount))]
    public class QuantityDiscountDto : BaseAuditedDto
    {
        public int DiscountPercent { get; set; }
        public int MinQuantity { get; set; }
        public string SelectedEvents { get; set; }
    }
}