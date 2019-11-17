using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Dto
{
    public class WorkerFilterDto : YPagedAndSortDto
    {
        public int? JobLocationId { get; set; }
        public int? CategoryId { get; set; }
    }
}
