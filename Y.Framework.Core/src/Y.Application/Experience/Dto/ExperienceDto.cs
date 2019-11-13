using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    public class ExperienceDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public bool IsCurrentJob { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Description { get; set; }
        public int JobSeekerId { get; set; }
    }
}
