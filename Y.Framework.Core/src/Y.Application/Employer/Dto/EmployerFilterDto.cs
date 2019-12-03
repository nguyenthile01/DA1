using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    public class EmployerFilterDto : YPagedAndSortDto
    {
        public int? Id { get; set; }
        public string NameCompany { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
