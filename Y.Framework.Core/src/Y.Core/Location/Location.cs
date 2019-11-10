using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    [Table("Locations")]
    public class Location : BaseAuditedEntity, IMultiLingualEntity<LocationTranslation>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }


        public virtual ICollection<LocationTranslation> Translations { get; set; }

    }
}
