using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Dto
{
    public class DesiredCareerFilterDto : YPagedAndSortDto
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public int? JobSeekerId { get; set; }
    }
}
