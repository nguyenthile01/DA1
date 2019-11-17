using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(DesiredLocationJob))]
    public class CreateOrEditDesiredLocationJobDto : EntityDto
    {
        public int? JobSeekerId { get; set; }
        public int? CityId { get; set; }
    }
}
