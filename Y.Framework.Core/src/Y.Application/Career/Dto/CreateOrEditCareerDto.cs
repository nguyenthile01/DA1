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
    [AutoMap(typeof(Career))]
    public class CreateOrEditCareerDto : EntityDto
    {
        [MaxLength(EntityLength.Name)]
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}
