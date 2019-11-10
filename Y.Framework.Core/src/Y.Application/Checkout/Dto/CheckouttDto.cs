//using Abp.Application.Services.Dto;
//using Abp.AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using Y.Core;

//namespace Y.Dto
//{
//    [AutoMap(typeof(Order))]
//    public class CheckouttDto : EntityDto
//    {
//        #region General info

//        public Guid GuidCode { get; set; }
//        public string Code { get; set; }
//        public long? AccountId { get; set; }

//        [Display(Name = "Full name")]
//        public string RegisterFullName { get; set; }
//        [Display(Name = "City/Province")]
//        public string RegisterCity { get; set; }
//        [Display(Name = "Address")]
//        public string RegisterAddress { get; set; }

//        [Phone(ErrorMessage = "Invalid Phone Number")]
//        [Display(Name = "Phone")]
//        public string RegisterPhone { get; set; }

//        [EmailAddress(ErrorMessage = "Invalid Email Address")]
//        [Display(Name = "Email")]
//        public string RegisterEmail { get; set; }
//        [Display(Name = "Company")]
//        public string RegisterCompanyName { get; set; }

//        [Required]
//        [Display(Name = "Full name")]
//        public string ReceivingFullName { get; set; }

//        [Display(Name = "Address")]
//        public string ReceivingAddress { get; set; }

//        [Required]
//        [DataType(DataType.PhoneNumber)]
//        [Display(Name = "Phone")]
//        public string ReceivingPhone { get; set; }

//        [EmailAddress(ErrorMessage = "Invalid Email Address")]
//        [Display(Name = "Email")]
//        public string ReceivingEmail { get; set; }
//        [Display(Name = "Company")]
//        public string ReceivingCompanyName { get; set; }
//        [Display(Name = "Ghi chú")]
//        public string Note { get; set; }

//        [Display(Name = "Payment Method")]
//        public string PaymentMethod { get; set; }

//        public ReceiptStatus Status { get; set; }
//        public string StatusString
//        {
//            get { return Status.GetEnumDescription(); }
            
//        }
//        public DateTime CreationTime { get; set; }
//        #endregion

//        #region Checkout Details

//        public decimal SubTotal { get; set; }
//        public decimal Total { get; set; }
//        public decimal SurchargeTotal { get; set; }

//        #endregion

//        #region Transaction

//        public string TransactionId { get; set; }
//        public string ClientIp { get; set; }
//        //[Required]
//        [Display(Name = "Card type")]
//        public string CardType { get; set; }

//        #endregion

//        public virtual ICollection<OrderItem> CheckoutDetails { get; set; } = new HashSet<OrderItem>();
//        public virtual ICollection<Surcharge> Surcharges { get; set; } = new HashSet<Surcharge>();

//    }
//}
