using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Authorization.Users;

namespace Y.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class LoginViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
