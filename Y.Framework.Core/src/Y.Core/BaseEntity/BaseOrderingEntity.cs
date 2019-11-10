using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Y.Core
{
    public class BaseOrderingEntity : BaseEntity
    {
        public virtual int DisplayOrder { get; set; }
    }
}
