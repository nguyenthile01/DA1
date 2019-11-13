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
    public class ExperienceAppService : YAppServiceBase
    {
        private readonly IRepository<Experience> experienceRepository;
        public ExperienceAppService(IRepository<Experience> experienceRepository
        )
        {
            this.experienceRepository = experienceRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Experience)]
        public virtual async Task<PagedResultDto<ExperienceDto>> GetAll(ExperienceFilterDto input)
        {

            var query = experienceRepository.GetAll()
                 .WhereIf(input.Id != null, p => p.Id == input.Id);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<ExperienceDto>(
                totalCount,
                entities.Select(p => p.MapTo<ExperienceDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditExperienceDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditExperienceDto();
            if (id == null)
            {
                return model;
            }

            var entity = await experienceRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Experience)]
        public async Task CreateOrUpdate(CreateOrEditExperienceDto input)
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
        //[AbpAuthorize(PermissionNames.AdminPage_Experience)]
        public async Task Delete(EntityDto<int> input)
        {
            await experienceRepository.DeleteAsync(p => p.Id == input.Id);

        }
        protected virtual async Task UpdateAsync(CreateOrEditExperienceDto input)
        {
            var entity = await experienceRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await experienceRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditExperienceDto input)
        {

            var entity = ObjectMapper.Map<Experience>(input);

            await experienceRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<Experience> ApplySorting(IQueryable<Experience> query, ExperienceFilterDto input)
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

        protected virtual IQueryable<Experience> ApplyPaging(IQueryable<Experience> query, ExperienceFilterDto input)
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
