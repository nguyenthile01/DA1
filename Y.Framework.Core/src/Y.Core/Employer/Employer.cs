using System;
using System.Collections.Generic;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class Employer:BaseAuditedEntity
    {
        public string NameCompany { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

    }
}
