﻿using System.Collections.Generic;
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
        private readonly IRepository<DesiredLocationJob> locationJobRepository;
        private readonly IRepository<DesiredCareer> jobCategoryRepository;
        private readonly IRepository<Experience> experienceRepository;
        //table jobcategory la bang nao e? trong Y.Core hay Y.App
        public JobSeekerAppService(IRepository<JobSeeker> jobSeekerRepository,
            IRepository<DesiredLocationJob> locationJobRepository,
            IRepository<DesiredCareer> jobCategoryRepository,
            IRepository<Experience> experienceRepository
        )
        {
            this.jobSeekerRepository = jobSeekerRepository;
            this.locationJobRepository = locationJobRepository;
            this.jobCategoryRepository = jobCategoryRepository;
            this.experienceRepository = experienceRepository;
        }

        //[AbpAuthorize(PermissionNames.AdminPage_JobSeeker)]
        public virtual async Task<PagedResultDto<JobSeekerDto>> GetAll(JobSeekerFilterDto input)
        {
            
            //giờ lấy danh sách serkerid từ category
            var jobseekerWithCategoryIds = jobCategoryRepository
                 .GetAll()
                 .WhereIf(input.CategoryId != null, p => p.CategoryId == input.CategoryId)
                 .Select(p => p.JobSeekerId)
                 .Distinct()
                 .ToList();

            //lấy danh sách seekerid từ cityid

            var jobseekerWithCityIds = locationJobRepository
                .GetAll()
                .WhereIf(input.JobLocationId != null, p => p.CityId == input.JobLocationId)
                .Select(p => p.JobSeekerId)
                .Distinct()
                .ToList();

            //giao 3 mảng lại dc danh sách seekerids
            //chỗ này e tìm cách viết ra 1 tập hơp jobseekerids
            //lấy data từ danh sách đó

            var jobseekerids = jobseekerWithCityIds.Intersect(jobseekerWithCategoryIds);
            //jobseekerids = jobseekerids.Intersect(jobseekerWithExperince);


            var query = jobSeekerRepository
                .GetAll()
                .Where(p => jobseekerids.Contains(p.Id))
                .Where(p=>p.IsActive == true);


            //var query = jobSeekerRepository.GetAll()
            //     .WhereIf(input.Id != null, p => p.Id == input.Id);

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
