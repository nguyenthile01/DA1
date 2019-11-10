using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(EmailTemplate))]
    public class EmailTemplateUpdateDto : EntityDto
    {
        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }
    }
}
