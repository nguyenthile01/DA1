using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(EmailTemplate))]
    public class EmailTemplateCreateDto : EntityDto
    {
        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }

        public virtual ICollection<EmailTemplateTranslation> Translations { get; set; }
    }
}
