using System;
using System.Collections.Generic;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class Knowledge:BaseAuditedEntity
    {
        public string Specialized { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Achievement { get; set; }
    }
}
