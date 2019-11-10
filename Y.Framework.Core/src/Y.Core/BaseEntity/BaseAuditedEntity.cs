using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Y.Core
{
    public class BaseAuditedEntity : BaseOrderingEntity, IAudited, ISoftDelete, IPassivable
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime CreationTime { get; set; } = Clock.Now;
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
