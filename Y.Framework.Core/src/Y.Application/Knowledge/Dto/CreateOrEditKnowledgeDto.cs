using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
    [AutoMap(typeof(Knowledge))]
    public class CreateOrEditKnowledgeDto : EntityDto
    {
        public string Specialized { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Achievement { get; set; }
        public int? JobSeekerId { get; set; }
    }
}
