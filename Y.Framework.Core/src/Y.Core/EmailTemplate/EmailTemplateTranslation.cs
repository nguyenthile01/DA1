using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Y.Authorization.Users;

namespace Y.Core
{
    public class EmailTemplateTranslation : SeoBaseEntity<int, User>, IEntityTranslation<EmailTemplate>, IEntity
    {
        public string Body { get; set; }

        public EmailTemplate Core { get; set; }
        public int CoreId { get; set; }
        public string Language { get; set; }
    }
}
