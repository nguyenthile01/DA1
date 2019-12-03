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
        public string NameCompany { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
 
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }
    }
}
