using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class JobSeeker : BaseAuditedEntity
    {
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string avtarUrl { get; set; }
        public string Desc { get; set; }
        public string ProfessionalTitle { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
