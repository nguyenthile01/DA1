using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Y.Core;
using Y.Services;

namespace Y.Dto
{
    public class ContactExcelDto
    {
        [DisplayName("Tên người liên hệ")]
        public string FullName { get; set; }
        [DisplayName("Email người liên hệ")]
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [DisplayName("Công ty")]
        public string Company { get; set; }
        [DisplayName("Nội dung")]
        public string Content { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime CreationTime { get; set; }

    }
}