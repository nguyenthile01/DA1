using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class DesiredCareer:BaseAuditedEntity
    {
        [ForeignKey(nameof(CategoryId))]
        public JobCategory JobCategory { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(JobSeekerId))]
        public JobSeeker JobSeeker { get; set; }
        public int? JobSeekerId { get; set; }
    }
}
