//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Domain.Entities;
//using Abp.Domain.Entities.Auditing;
//using Y.Authorization.Users;

//namespace Y.Core
//{
//    public class PaymentRequest : BaseAuditedEntity
//    {
//        public string TransactionReferent { get; set; }
//        public string AccessKey { get; set; }
//        public string Amount { get; set; }
//        public decimal AmountNumber { get; set; }
//        public string Command { get; set; }
  
//        public string OrderInfo { get; set; }
//        public string ReturnUrl { get; set; }
//        public string SecureHash { get; set; }
//        public string PayUrl { get; set; }
//        public string Status { get; set; }
//        public string PaymentGatewayProvider { get; set; }
//        public int? EntityId { get; set; }
//        [MaxLength(30)]
//        public string EntityName { get; set; }
//        [MaxLength(50)]
//        public string PaymentGateWay{ get; set; }

//        #region OnePay
//        public string Version { get; set; }
//        public string Currency { get; set; }
//        public string Merchant { get; set; }
//        public string Locale { get; set; }
//        public string TicketNumber { get; set; }
//        public string AgainLink { get; set; }
//        public string Title { get; set; }
//        public string Street { get; set; }
//        public string Province { get; set; }
//        public string City { get; set; }
//        public string Country { get; set; }
//        public string Phone { get; set; }
//        public string Email { get; set; }
//        public long? CustomerId { get; set; }
//        #endregion

//        #region Response
//        public string ResponseCode { get; set; }
//        public string ResponseMessage { get; set; }
//        public string TransactionNumber { get; set; }
//        #endregion

//        #region Reference
//        public int OrderId { get; set; }

//        [ForeignKey(nameof(OrderId))]
//        public virtual Order Order { get; set; }

//        #endregion
//    }
//}