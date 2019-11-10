using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(NewsLetterSubcription))]
    public class NewsLetterSubcriptionCreateDto
    {

        [MaxLength(EntityLength.Email)]
        public string Email { get; set; }

    }
}