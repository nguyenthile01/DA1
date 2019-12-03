using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    public class JobSeekerFilterDto : YPagedAndSortDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? JobLocationId { get; set; }
        public int? CategoryId { get; set; }
        public string YearsOfExperience { get; set; }
        public string ProfessionalTitle { get; set; }

    }
}
