using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    public class Contact : BaseAuditedEntity
    {

        [Display(Name = "Loại liên hệ")]
        public string ContactType { get; set; }

        [Required]
        [MaxLength(EntityLength.Name)]
        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }
        [MaxLength(EntityLength.Email)]
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(EntityLength.Phone)]
        [Display(Name = "Điện thoại")]
        [Required]
        public string Phone { get; set; }
        [MaxLength(EntityLength.Name)]

        [Display(Name = "Công ty")]
        public string Company { get; set; }

        //[Required]
        [MaxLength(EntityLength.ShortDescription)]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

    }
}