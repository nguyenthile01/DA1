using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class JobApplication:BaseAuditedEntity
    {
        [ForeignKey(nameof(JobSeekerId))]
        public JobSeeker JobSeeker { get; set; }
        public int? JobSeekerId { get; set; }
        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; }
        public int? JobId { get; set; }
    }
}
