using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Dto
{
    public class LocationJobFilterDto : YPagedAndSortDto
    {
        public int? Id { get; set; }
        public int? JobSeekerId { get; set; }
        public int? CityId { get; set; }
    }
}
