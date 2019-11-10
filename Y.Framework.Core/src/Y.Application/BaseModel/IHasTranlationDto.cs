using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace Y.Dto
{
    public interface IHasTranlationDto<T> where T : ITranslationDto
    {
        List<T> Translations { get; set; }
    }
}
