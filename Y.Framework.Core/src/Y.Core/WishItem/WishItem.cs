//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Domain.Entities;
//using Abp.Domain.Entities.Auditing;
//using Y.Authorization.Users;
//using Y.Core;

//namespace Y.Core
//{
//    [Table("WishItems")]

//    public class WishItem : BaseAuditedEntity
//    {
//        [ForeignKey("User")]
//        public long UserId { get; set; }
//        public virtual User User { get; set; }
//        [ForeignKey("Event")]
//        public int EventId { get; set; }
//        public virtual Event Event { get; set; }
//    }
//}
