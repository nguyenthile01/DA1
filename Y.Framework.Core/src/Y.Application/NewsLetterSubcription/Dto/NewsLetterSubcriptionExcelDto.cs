using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
   
    public class NewsLetterSubcriptionExcelDto
    {
        [DisplayName("Email liên hệ")]
        public string Email { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime CreationTime { get; set; }

    }
}