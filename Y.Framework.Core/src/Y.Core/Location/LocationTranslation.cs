using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Y.Authorization.Users;

namespace Y.Core
{
    public class LocationTranslation : SeoBaseEntity<int, User>,IEntityTranslation<Location>, IEntity
    {
        [MaxLength(EntityLength.Name)]
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual Location Core { get; set; }
        public int CoreId { get; set; }
        public string Language { get; set; }
    }
}
