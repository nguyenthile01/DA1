using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Dto
{
    public class JobFilterDto: YPagedAndSortDto
    {
        public int? Id { get; set; }
        public int? JobCategoryId { get; set; }
        public string Title { get; set; }
    }
}
