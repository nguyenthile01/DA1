using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    [Table("EmailTemplates")]
    public class EmailTemplate : BaseAuditedEntity, IMultiLingualEntity<EmailTemplateTranslation>
    {
        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }

        public ICollection<EmailTemplateTranslation> Translations { get; set; }
    }
}
