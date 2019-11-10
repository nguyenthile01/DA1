using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    public class CheckoutFilterDto : PagedAndSortedResultRequestDto
    {
        public string Keyword  { get; set; }
        public ReceiptStatus? Status { get; set; }
    }
}