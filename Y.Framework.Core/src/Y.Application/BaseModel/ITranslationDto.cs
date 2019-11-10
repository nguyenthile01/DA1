using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace Y.Dto
{
    public interface ITranslationDto
    {
        string Language { get; set; }
    }
}
