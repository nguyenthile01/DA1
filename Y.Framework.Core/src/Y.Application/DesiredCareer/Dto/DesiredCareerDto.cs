using System;
using System.Collections.Generic;
using System.Text;
using Y.Dto;
using Y.Core;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Y.Dto
{
    [AutoMap(typeof(DesiredCareer))]
    public class DesiredCareerDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public int JobSeekerId { get; set; }
        public int JobCategoryId { get; set; }
    }
}
