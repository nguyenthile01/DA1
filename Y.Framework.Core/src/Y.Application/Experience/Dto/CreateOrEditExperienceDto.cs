using System;
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
    [AutoMap(typeof(Experience))]
    public class CreateOrEditExperienceDto : EntityDto
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public bool IsCurrentJob { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Description { get; set; }
        public int? JobSeekerId { get; set; }
    }
}
