using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Y.Core
{
    public class BaseAuditedHardDeleteEntity : BaseOrderingEntity, IAudited
    {
        public DateTime CreationTime { get; set; } = Clock.Now;
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
