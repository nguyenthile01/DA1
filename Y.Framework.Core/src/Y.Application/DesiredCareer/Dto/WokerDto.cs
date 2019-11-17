using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    public class WokerDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string AvtarUrl { get; set; }
    }
}
