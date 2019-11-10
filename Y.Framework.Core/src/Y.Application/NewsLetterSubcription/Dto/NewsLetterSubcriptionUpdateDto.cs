using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
    [AutoMap(typeof(NewsLetterSubcription))]
    public class NewsLetterSubcriptionUpdateDto : BaseUpdateDto
    {
        [MaxLength(EntityLength.Email)]
        public string Email { get; set; }
    }
}