//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Domain.Entities;
//using Abp.Domain.Entities.Auditing;
//using Y.Authorization.Users;

//namespace Y.Core
//{
//    [Table("ShoppingCarts")]
//    public class ShoppingCart : BaseAuditedHardDeleteEntity
//    {
//        public long? UserId { get; set; }
//        [MaxLength(EntityLength.UserCookie)]
//        public string UserCookie { get; set; }
//        [ForeignKey(nameof(UserId))]
//        public virtual User User { get; set; }

//        public virtual ICollection<ShoppingCartItem> CartItems { get; set; }
//    }
//}