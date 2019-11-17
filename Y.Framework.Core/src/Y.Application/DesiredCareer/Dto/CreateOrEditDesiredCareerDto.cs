using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(DesiredCareer))]
    public class CreateOrEditDesiredCareerDto : EntityDto
    {
        public int? CategoryId { get; set; }
        public int? JobSeekerId { get; set; }
    }
}
