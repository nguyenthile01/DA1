﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;
using System.Text;

namespace Y.Dto
{
    [AutoMap(typeof(Employer))]
    public class EmployerDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public string NameCompany { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
