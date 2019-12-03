using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    public class ExperienceFilterDto : YPagedAndSortDto
    {
        public int? Id { get; set; }
        public int? JobSeekerId { get; set; }
        public string YearsOfExperience { get; set; }
    }
}
