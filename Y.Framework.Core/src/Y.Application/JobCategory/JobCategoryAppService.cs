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
   public class JobCategoryAppService : YAppServiceBase
    {
        private readonly IRepository<JobCategory> jobCategoryRepository;
        public JobCategoryAppService(IRepository<JobCategory> jobCategoryRepository
        )
        {
            this.jobCategoryRepository = jobCategoryRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_JobCategory)]
        public virtual async Task<PagedResultDto<JobCategoryDto>> GetAll(JobCategoryFilterDto input)
        {

            var query = jobCategoryRepository.GetAll()
                 .WhereIf(input.Id != null, p => p.Id == input.Id);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<JobCategoryDto>(
                totalCount,
                entities.Select(p => p.MapTo<JobCategoryDto>())
                    .ToList()
            );
        }
        //[AbpAuthorize(PermissionNames.AdminPage_JobCategory)]
        public async Task CreateOrUpdate(CreateOrEditJobCategoryDto input)
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
        //[AbpAuthorize(PermissionNames.AdminPage_JobCategory)]
        public async Task Delete(EntityDto<int> input)
        {
            await jobCategoryRepository.DeleteAsync(p => p.Id == input.Id);

        }
        public void Delete1(EntityDto<int> input)
        {
            jobCategoryRepository.Delete(input.Id);
            
            CurrentUnitOfWork.SaveChanges();
        }
        protected virtual async Task UpdateAsync(CreateOrEditJobCategoryDto input)
        {
            var entity = await jobCategoryRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await jobCategoryRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditJobCategoryDto input)
        {

            var entity = ObjectMapper.Map<JobCategory>(input);

            await jobCategoryRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<JobCategory> ApplySorting(IQueryable<JobCategory> query, JobCategoryFilterDto input)
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

        protected virtual IQueryable<JobCategory> ApplyPaging(IQueryable<JobCategory> query, JobCategoryFilterDto input)
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
