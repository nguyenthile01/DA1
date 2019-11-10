using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(Topic))]
    public class TopicDto : IEntityDto<int>
    {
        public int Id { get; set; }

        #region Localize

        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        [MaxLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
    
        #endregion

        public string CategoryName { get; set; }
        public DateTime CreationTime { get; set; } 
        public bool IsActive { get; set; } 

    }
}
