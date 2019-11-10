using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(EmailTemplateTranslation))]
    public class EmailTemplateTranslationDto : SeoBaseDto
    {
        public string Body { get; set; }

        public string Language { get; set; }

    }
}
