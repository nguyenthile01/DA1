using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Y.Dto
{
    public class ContactFilterDto : YPagedAndSortDto
    {
       
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

    }
}