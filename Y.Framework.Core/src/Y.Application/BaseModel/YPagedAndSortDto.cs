using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    public class YPagedAndSortDto : PagedAndSortedResultRequestDto
    {
        public int Page { get; set; } = 1;
        public int RowsPerPage { get; set; } = Int32.MaxValue;
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public override string Sorting => $"{SortBy} {SortDirection}".Trim();
        public override int SkipCount => (Page - 1) * RowsPerPage;
        public override int MaxResultCount => RowsPerPage;
    }
}
