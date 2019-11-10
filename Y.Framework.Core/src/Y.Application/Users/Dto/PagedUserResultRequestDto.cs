using Abp.Application.Services.Dto;
using System;
using Y.Dto;

namespace Y.Users.Dto
{
    //custom PagedResultRequestDto
    public class PagedUserResultRequestDto : YPagedAndSortDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
