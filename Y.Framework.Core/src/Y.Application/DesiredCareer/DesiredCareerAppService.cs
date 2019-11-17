using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Y.Authorization;
using Y.Core;
using Y.Dto;

namespace Y.Services
{
    public class DesiredCareerAppService : YAppServiceBase
    {
        private readonly IRepository<DesiredCareer> desiredCareerRepository;
        public DesiredCareerAppService(IRepository<DesiredCareer> desiredCareerRepository
        )
        {
            this.desiredCareerRepository = desiredCareerRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_DesiredCareer)]
        public virtual async Task<PagedResultDto<DesiredCareerDto>> GetAll(DesiredCareerFilterDto input)
        {

            var query = desiredCareerRepository.GetAll()
                .Include(p=>p.JobSeeker)
                .Include(p=>p.JobCategory)
                 .WhereIf(input.Id != null, p => p.Id == input.Id)
                 .WhereIf(input.CategoryId != null, p => p.CategoryId == input.CategoryId)
                 .WhereIf(input.JobSeekerId != null, p => p.JobSeekerId == input.JobSeekerId);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<DesiredCareerDto>(
                totalCount,
                entities.Select(p => p.MapTo<DesiredCareerDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditDesiredCareerDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditDesiredCareerDto();
            if (id == null)
            {
                return model;
            }

            var entity = await desiredCareerRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_DesiredCareer)]
        public async Task CreateOrUpdate(CreateOrEditDesiredCareerDto input)
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
        //[AbpAuthorize(PermissionNames.AdminPage_DesiredCareer)]
        public async Task Delete(EntityDto<int> input)
        {
            await desiredCareerRepository.DeleteAsync(p => p.Id == input.Id);

        }
        protected virtual async Task UpdateAsync(CreateOrEditDesiredCareerDto input)
        {
            var entity = await desiredCareerRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await desiredCareerRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditDesiredCareerDto input)
        {

            var entity = ObjectMapper.Map<DesiredCareer>(input);

            await desiredCareerRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<DesiredCareer> ApplySorting(IQueryable<DesiredCareer> query, DesiredCareerFilterDto input)
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

        protected virtual IQueryable<DesiredCareer> ApplyPaging(IQueryable<DesiredCareer> query, DesiredCareerFilterDto input)
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
