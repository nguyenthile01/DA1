using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Y.Core;
using Y.Services;


namespace Y.Dto
{
    [AutoMap(typeof(JobSeeker))]
    public class CreateOrEditJobSeekerDto : EntityDto
    {
        [MaxLength(EntityLength.Name)]
        [Required]
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        [MaxLength(EntityLength.Name)]
        [Required]
        public string Name { get; set; }
        public string UserName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

    }
}
