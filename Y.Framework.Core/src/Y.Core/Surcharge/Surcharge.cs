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
//    public class Surcharge : BaseAuditedEntity
//    {
//        [MaxLength(EntityLength.Name)]
//        public string SurchargeType { get; set; }
//        [MaxLength(EntityLength.Name)]
//        public string Name { get; set; }
//        [MaxLength(EntityLength.ShortDescription)]
//        public string Description { get; set; }

//        public decimal Amount { get; set; }

//        [MaxLength(EntityLength.Name)]
//        public string ExtraInfos { get; set; }

//        public int OrderId { get; set; }

//        [ForeignKey(nameof(OrderId))]
//        public virtual Order Order { get; set; }


//    }
//}
