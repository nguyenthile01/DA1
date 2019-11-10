﻿using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    public class TopicFilterDto : YPagedAndSortDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? TopicCategoryId { get; set; }
        public string CategoryName  { get; set; }
    }
}