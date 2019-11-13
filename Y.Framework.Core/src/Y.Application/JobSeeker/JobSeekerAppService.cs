using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Y.Authorization;
using Y.Core;
using Y.Dto;

namespace Y.Services
{
   public class JobSeekerAppService : YAppServiceBase
    {
        private readonly IRepository<JobSeeker> jobSeekerRepository;
        public JobSeekerAppService(IRepository<JobSeeker> jobSeekerRepository
        )
        {
            this.jobSeekerRepository = jobSeekerRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_JobSeeker)]
        public virtual async Task<PagedResultDto<JobSeekerDto>> GetAll(JobSeekerFilterDto input)
        {

            var query = jobSeekerRepository.GetAll()
                 .WhereIf(input.Id != null, p => p.Id == input.Id);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<JobSeekerDto>(
                totalCount,
                entities.Select(p => p.MapTo<JobSeekerDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditJobSeekerDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditJobSeekerDto();
            if (id == null)
            {
                return model;
            }

            var entity = await jobSeekerRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_JobSeeker)]
        public async Task CreateOrUpdate(CreateOrEditJobSeekerDto input)
        {
            if (input.Id != 0)
            {
                await UpdateAsync(input);
            }
            else
            {
                await CreateUserAsync(input);
            }
        }
        //[AbpAuthorize(PermissionNames.AdminPage_JobSeeker)]
        public async Task Delete(EntityDto<int> input)
        {
            await jobSeekerRepository.DeleteAsync(p => p.Id == input.Id);

        }
        protected virtual async Task UpdateAsync(CreateOrEditJobSeekerDto input)
        {
            var entity = await jobSeekerRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await jobSeekerRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditJobSeekerDto input)
        {

            var entity = ObjectMapper.Map<JobSeeker>(input);

            await jobSeekerRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<JobSeeker> ApplySorting(IQueryable<JobSeeker> query, JobSeekerFilterDto input)
        {
            var sortInput = input as ISortedResultRequest;
            if (sortInput != null)
            {
                if (sortInput.Sorting.IsNotNullOrEmpty())
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending(e => e.Id);
            }

            return query;
        }

        protected virtual IQueryable<JobSeeker> ApplyPaging(IQueryable<JobSeeker> query, JobSeekerFilterDto input)
        {
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return query.PageBy(pagedInput);
            }

            var limitedInput = input as ILimitedResultRequest;
            if (limitedInput != null)
            {
                return query.Take(limitedInput.MaxResultCount);
            }

            return query;
        }
    }
}
