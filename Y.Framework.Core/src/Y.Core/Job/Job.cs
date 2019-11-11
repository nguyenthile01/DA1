using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Y.Core;

namespace Y.Core
{
    public class Job:BaseAuditedEntity
    {
        [ForeignKey(nameof(CategoryId))]
        public JobCategory JobCategory { get; set; }
        public int? CategoryId { get; set; }

        public int AmountOfPeople { get; set; }
        public string Title { get; set; }
        public string RankAtWork { get; set; }
        public int Wage { get; set; }

        [ForeignKey(nameof(CityId))]
        public City Cities { get; set; }
        public int? CityId { get; set; }

        public string DescJob { get; set; }
        public string WorkExperience { get; set; }
        public string Degree { get; set; }
        public string Gender { get; set; }
        public DateTime DeadlineForSubmission { get; set; } = DateTime.Now;
        public string ProfileLanguage { get; set; }
        public string JobRequirement { get; set; }
        
    }
}
