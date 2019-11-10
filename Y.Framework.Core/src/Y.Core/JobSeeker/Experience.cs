using System;
using System.Collections.Generic;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class Experience:BaseAuditedEntity
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public bool IsCurrentJob { get; set; } = true;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Description { get; set; }
    }
}
