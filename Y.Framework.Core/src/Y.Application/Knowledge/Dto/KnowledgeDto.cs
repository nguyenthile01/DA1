using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    public class KnowledgeDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public string Specialized { get; set; }
        public string School { get; set; }
        public string Qualification { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Achievement { get; set; }
        public int JobSeekerId { get; set; }
    }
}
