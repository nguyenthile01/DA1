using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Core
{
    public class QuantityDiscount : BaseAuditedEntity
    {
        public int DiscountPercent { get; set; }
        public int MinQuantity { get; set; }
        public string SelectedEvents { get; set; }
    }
}