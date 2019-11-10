using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Y.Dto;

namespace Y.Services
{
    public interface INewsLetterSubcriptionAppService
    {
        List<NewsLetterSubcriptionExcelDto> GetExcelData(NewsLetterSubcriptionFilter input);
    }
}
