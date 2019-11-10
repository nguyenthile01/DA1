using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
    [AutoMapFrom(typeof(Contact))]
    public class ContactDto: BaseAuditedDto
    {
        public string ContactType { get; set; }

        [MaxLength(EntityLength.Name)]
        public string FullName { get; set; }

        [MaxLength(EntityLength.Email)]
        public string Email { get; set; }

        [MaxLength(EntityLength.Phone)]
        public string Phone { get; set; }

        [MaxLength(EntityLength.Name)]
        public string Company { get; set; }

        [MaxLength(EntityLength.ShortDescription)]
        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

    }
}