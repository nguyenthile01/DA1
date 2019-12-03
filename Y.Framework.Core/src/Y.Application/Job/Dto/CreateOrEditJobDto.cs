using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(Job))]
    public class CreateOrEditJobDto: EntityDto
    {
        public int CategoryId { get; set; }

        public int AmountOfPeople { get; set; }
        public string Title { get; set; }
        public string RankAtWork { get; set; }
        public int Wage { get; set; }


        public int CityId { get; set; }
        public int EmployerId { get; set; }

        public string DescJob { get; set; }
        public string WorkExperience { get; set; }
        public string Degree { get; set; }
        public string Gender { get; set; }
        public DateTime DeadlineForSubmission { get; set; }
        public string ProfileLanguage { get; set; }
        public string JobRequirement { get; set; }
        public bool IsActive { get; set; }
    }
}
