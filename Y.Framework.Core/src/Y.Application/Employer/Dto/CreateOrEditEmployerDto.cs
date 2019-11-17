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
    [AutoMap(typeof(Employer))]
    public class CreateOrEditEmployerDto : EntityDto
    {
        [MaxLength(EntityLength.Name)]
        [Required]
        public string Name { get; set; }
        [MaxLength(EntityLength.Name)]
        [Required]
        public string NameCompany { get; set; }
        [MaxLength(EntityLength.Name)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(EntityLength.Name)]
        [Required]
        public string EmailAddress { get; set; }
        [MaxLength(EntityLength.Name)]
        [Required]
        public string PhoneNumber { get; set; }
        [MaxLength(EntityLength.Name)]
        [Required]
        public string Password { get; set; }
        [MaxLength(EntityLength.Name)]
        [Required]
        public string Address { get; set; }
    }
}
