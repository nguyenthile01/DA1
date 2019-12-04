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
    public class JobAppService : YAppServiceBase
    {
        private readonly IRepository<Job> jobRepository;
        public JobAppService(IRepository<Job> jobRepository
        )
        {
            this.jobRepository = jobRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Job)]
        public virtual async Task<PagedResultDto<JobDto>> GetAll(JobFilterDto input)
        {

            var query = jobRepository.GetAll()
                .Include(p => p.JobCategory)
                 .WhereIf(input.Id != null, p => p.Id == input.Id)
                   .WhereIf(input.JobCategoryId != null, p => p.JobCategoryId == input.JobCategoryId)
                   .WhereIf(input.Title != null, p => p.Title == input.Title);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<JobDto>(
                totalCount,
                entities.Select(p => p.MapTo<JobDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditJobDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditJobDto();
            if (id == null)
            {
                return model;
            }

            var entity = await jobRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Job)]
        public async Task CreateOrUpdate(CreateOrEditJobDto input)
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
        //[AbpAuthorize(PermissionNames.AdminPage_Job)]
        public async Task Delete(EntityDto<int> input)
        {
            await jobRepository.DeleteAsync(p => p.Id == input.Id);

        }
        public void Delete1(EntityDto<int> input)
        {
            jobRepository.Delete(input.Id);

            CurrentUnitOfWork.SaveChanges();
        }
        protected virtual async Task UpdateAsync(CreateOrEditJobDto input)
        {
            var entity = await jobRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await jobRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditJobDto input)
        {

            var entity = ObjectMapper.Map<Job>(input);

            await jobRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<Job> ApplySorting(IQueryable<Job> query, JobFilterDto input)
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

        protected virtual IQueryable<Job> ApplyPaging(IQueryable<Job> query, JobFilterDto input)
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
