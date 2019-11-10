//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Domain.Entities;
//using Abp.Domain.Entities.Auditing;
//using Y.Authorization.Users;

//namespace Y.Core
//{
//    public class EventOrganizer : BaseAuditedEntity
//    {
//        public int EventId { get; set; }
//        public int OrganizerId { get; set; }
//        [ForeignKey(nameof(OrganizerId))]
//        public virtual Organizer Organizer { get; set; }
//        [ForeignKey(nameof(EventId))]
//        public virtual Event Event { get; set; }
//    }
//}
