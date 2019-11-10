//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Domain.Entities;
//using Abp.Domain.Entities.Auditing;
//using Y.Authorization.Users;

//namespace Y.Core
//{
//    public class PaymentResult : BaseAuditedEntity
//    {
//        public string TransactionReferent { get; set; }
//        public string PayUrl { get; set; }
//        public string Status { get; set; }
//        public string AccessKey { get; set; }
//        public string Amount { get; set; }
//        public decimal AmountNumber { get; set; }
//        public string CardName { get; set; }
//        public string CardType { get; set; }
//        public string OrderType { get; set; }
//        public string RequestTime { get; set; }
//        public string ResponseCode { get; set; }
//        public string ResponseMessage { get; set; }
//        public string ResponseTime { get; set; }
//        public string TransactionStatus { get; set; }
//        public int OrderId { get; set; }
//        //this is securehash in onepay
//        public string Signature { get; set; }
//        public string BankCode { get; set; }
//        public string BankTranNo { get; set; }
//        public string PayDate { get; set; }
//        public string TmnCode { get; set; }
//        public string TransactionNo { get; set; }
//        public string TxnRef { get; set; }
//        public string SecureHashType { get; set; }
//        public string SecureHash { get; set; }
//        [MaxLength(50)]
//        public string PaymentGateWay { get; set; }
//        #region OnePay
//        public string Command { get; set; }
//        public string Locale { get; set; }
//        public string CurrencyCode { get; set; }
//        public string Merchant { get; set; }
//        public string OrderInfo { get; set; }
//        public string TransactionNumber { get; set; }
//        public string Message { get; set; }

//        #endregion
//        [ForeignKey("PaymentRequest")]
//        public int PaymentRequestId { get; set; }
//        public virtual PaymentRequest PaymentRequest { get; set; }
//    }
//}