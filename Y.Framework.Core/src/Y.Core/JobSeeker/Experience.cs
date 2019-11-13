﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class Experience : BaseAuditedEntity
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public bool IsCurrentJob { get; set; } = false;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(JobSeekerId))]
        public JobSeeker JobSeeker { get; set; }
        public int JobSeekerId { get; set; }
    }
}
