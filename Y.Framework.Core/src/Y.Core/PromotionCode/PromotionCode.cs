using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Core
{
    public class PromotionCode : BaseAuditedEntity
    {
        [NotMapped]
        public static int PublicCode = -1;
        [MaxLength(200)]
        public string DiscountCode { get; set; }
        public int ActiveCount { get; set; }
        [MaxLength(100)]
        public string DiscountAmountType { get; set; }
        public int ActiveCountRemain { get; set; }
        public decimal DiscountAmount { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public string SelectedEvents { get; set; }

        public bool ApplyForAllEvents { get; set; } = false;

    }
}