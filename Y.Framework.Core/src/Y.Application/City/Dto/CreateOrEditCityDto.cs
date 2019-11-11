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
    [AutoMap(typeof(City))]
    public class CreateOrEditCityDto : EntityDto
    {
        [MaxLength(EntityLength.Name)]
        [Required]
        public string Name { get; set; }
    }
}
