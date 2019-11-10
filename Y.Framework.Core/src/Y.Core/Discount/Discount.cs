using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Core
{
    public class Discount : BaseAuditedEntity
    {
        public DateTime ActiveToDate { get; set; }

        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        public int? PromotionPostId { get; set; }
        [MaxLength(EntityLength.Url)]
        public string AvatarId { get; set; }
    }
}