using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    public class KnowledgeFilterDto : YPagedAndSortDto
    {
        public int? Id { get; set; }
        public int? JobSeekerId { get; set; }
    }
}
