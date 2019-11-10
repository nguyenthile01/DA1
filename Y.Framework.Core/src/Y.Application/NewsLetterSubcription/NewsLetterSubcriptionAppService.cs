using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Y.Core;
using Y.Dto;

namespace Y.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class NewsLetterSubcriptionAppService : AsyncCrudAppService<NewsLetterSubcription, NewsLetterSubcriptionDto, int,
        NewsLetterSubcriptionFilter, NewsLetterSubcriptionCreateDto, NewsLetterSubcriptionUpdateDto>, INewsLetterSubcriptionAppService
    {
        private readonly IRepository<NewsLetterSubcription> newsLetterRepository;
        private readonly IEmailSender emailSender;

        public NewsLetterSubcriptionAppService(IRepository<NewsLetterSubcription> newsLetterRepository, IEmailSender emailSender) : base(newsLetterRepository)
        {
            this.emailSender = emailSender;
            this.newsLetterRepository = newsLetterRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Contact)]
        public List<NewsLetterSubcriptionExcelDto> GetExcelData(NewsLetterSubcriptionFilter input)
        {
            return newsLetterRepository.GetAll()
                        .Where(p => p.IsDeleted == false)
                        .ToList()
                        .Select(p => p.MapTo<NewsLetterSubcriptionExcelDto>())
                        .ToList();
        }
        public override async Task<PagedResultDto<NewsLetterSubcriptionDto>> GetAll(NewsLetterSubcriptionFilter input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input)
                   .OrderByDescending(p=>p.CreationTime);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<NewsLetterSubcriptionDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );

        }
    }
}

