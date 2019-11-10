﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(Contact))]
    public class CreateOrEditContactDto
    {
        public string ContactType { get; set; }

        [MaxLength(EntityLength.Name)]
        public string FullName { get; set; }

        [MaxLength(EntityLength.Email)]
        public string Email { get; set; }

        [MaxLength(EntityLength.Phone)]
        public string Phone { get; set; }

        [MaxLength(EntityLength.Name)]
        public string Company { get; set; }

        [MaxLength(EntityLength.ShortDescription)]
        public string Content { get; set; }
    }
}