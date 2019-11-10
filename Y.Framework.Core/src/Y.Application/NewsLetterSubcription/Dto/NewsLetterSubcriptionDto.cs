using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
    [AutoMapFrom(typeof(NewsLetterSubcription))]
    public class NewsLetterSubcriptionDto: BaseAuditedDto
    {
      
        [Display(Name ="Email liên hệ")]
        public string Email { get; set; }

    }
}