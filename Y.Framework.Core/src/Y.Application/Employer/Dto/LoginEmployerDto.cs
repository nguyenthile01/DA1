using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Core;
using Y.Dto;

namespace Y.Dto
{
    [AutoMap(typeof(Employer))]
    public class LoginEmployerDto : EntityDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
