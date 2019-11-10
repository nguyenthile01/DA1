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
//    [Table("OrderItems")]
//    public class OrderItem : BaseAuditedEntity
//    {
//        public int OrderId { get; set; }
//        public int TicketPriceId { get; set; }
//        public int TicketTypeId { get; set; }
//        public int Quantity { get; set; }
//        public decimal Price { get; set; }
//        [ForeignKey(nameof(TicketPriceId))]
//        public virtual TicketPrice TicketPrice { get; set; }
//        [ForeignKey(nameof(TicketTypeId))]
//        public virtual TicketType TicketType { get; set; }
//        [ForeignKey(nameof(OrderId))]
//        public virtual Order Order { get; set; }
//    }
//}
