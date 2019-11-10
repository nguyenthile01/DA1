using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Y.Dto
{
    public class BaseUpdateDto : AuditedEntity<int>, IEntityDto<int>, ISoftDelete
    {
        public BaseUpdateDto()
        {
            LastModificationTime = Clock.Now;
        }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
