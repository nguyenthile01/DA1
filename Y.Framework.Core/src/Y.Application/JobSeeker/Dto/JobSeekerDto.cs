using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;
using System.Text;

namespace Y.Dto
{
    [AutoMap(typeof(JobSeeker))]
    public class JobSeekerDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string avtarUrl { get; set; }
        public bool IsActive { get; set; }
        public string ProfessionalTitle { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
