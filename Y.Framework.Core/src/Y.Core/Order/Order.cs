//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Authorization.Users;
//using Abp.Domain.Entities;
//using Abp.Domain.Entities.Auditing;
//using Y.Authorization.Users;

//namespace Y.Core
//{
//    [Table("Orders")]
//    public class Order : BaseAuditedEntity
//    {
//        #region General info
//        [MaxLength(EntityLength.UserCookie)]
//        public string UserCookie { get; set; }
//        public Guid GuidCode { get; set; }
//        [MaxLength(100)]
//        public string Code { get; set; }
//        public long? UserId { get; set; }
//        public int EventId { get; set; }

//        [MaxLength(EntityLength.Name)]
//        public string RegisterFullName { get; set; }
//        [MaxLength(EntityLength.Address)]
//        public string RegisterCity { get; set; }
//        public string RegisterAddress { get; set; }

//        [MaxLength(EntityLength.Phone)]
//        public string RegisterPhone { get; set; }

//        [MaxLength(EntityLength.Email)]
//        public string RegisterEmail { get; set; }
//        [MaxLength(EntityLength.Name)]
//        public string RegisterCompanyName { get; set; }

//        [MaxLength(EntityLength.Name)]
//        [Required]
//        public string ReceivingFullName { get; set; }

//        [MaxLength(EntityLength.Address)]
//        public string ReceivingAddress { get; set; }

//        [MaxLength(EntityLength.Phone)]
//        public string ReceivingPhone { get; set; }

//        [MaxLength(EntityLength.Email)]
//        public string ReceivingEmail { get; set; }
//        [MaxLength(EntityLength.Name)]
//        public string ReceivingCompanyName { get; set; }
//        [MaxLength(EntityLength.ShortDescription)]
//        public string Note { get; set; }

//        [MaxLength(100)]
//        public string PaymentMethod { get; set; }
//        //public bool IsPaid { get; set; }
//        public ReceiptStatus Status { get; set; }

//        #endregion

//        #region Checkout Details

//        public decimal SubTotal { get; set; }
//        public decimal Total { get; set; }
//        public decimal SurchargeTotal { get; set; }

//        #endregion

//        #region Transaction

//        [MaxLength(100)]
//        public string TransactionId { get; set; }
//        [MaxLength(100)]
//        public string ClientIp { get; set; }
//        [MaxLength(100)]
//        public string CardType { get; set; }

//        #endregion
//        [MaxLength(EntityLength.Name)]
//        public string Source { get; set; }
//        [MaxLength(EntityLength.Name)]
//        public string Merchant { get; set; }
//        [ForeignKey(nameof(UserId))]
//        public User User { get; set; }
//        [ForeignKey(nameof(EventId))]
//        public Event Event { get; set; }
//        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
//        public virtual ICollection<Surcharge> Surcharges { get; set; } = new HashSet<Surcharge>();

//        [NotMapped]
//        public string DiscountCode { get; set; }

//        public string UserLanguage { get; set; }
//    }
//}
