using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Y.Dto;

namespace Y.Services
{
    public interface IContactAppService
    {
        List<ContactExcelDto> GetExcelData(ContactFilterDto input);
    }
}
