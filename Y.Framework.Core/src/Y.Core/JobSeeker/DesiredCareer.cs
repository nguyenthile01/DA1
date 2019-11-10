using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class DesiredCareer:BaseAuditedEntity
    {
        [ForeignKey(nameof(CareerId))]
        public Career Career { get; set; }
        public int? CareerId { get; set; }

        [ForeignKey(nameof(JobSeekerId))]
        public JobSeeker JobSeeker { get; set; }
        public int? JobSeekerId { get; set; }
    }
}
