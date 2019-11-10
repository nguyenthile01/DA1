using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;
using System.Text;


namespace Y.Dto
{
    [AutoMap(typeof(JobCategory))]
    public class JobCategoryDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}
