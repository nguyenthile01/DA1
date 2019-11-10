using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class DesiredLocationJob:BaseAuditedEntity
    {
        [ForeignKey(nameof(CityId))]
        public Cities Cities { get; set; }
        public int? CityId { get; set; }
        [ForeignKey(nameof(JobSeekerId))]
        public JobSeeker JobSeeker { get; set; }
        public int? JobSeekerId { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
