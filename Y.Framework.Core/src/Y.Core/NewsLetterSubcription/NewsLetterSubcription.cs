using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    public class NewsLetterSubcription : BaseAuditedEntity
    {

        [MaxLength(EntityLength.Email)]
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}