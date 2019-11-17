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
    public class DesiredLocationJobAppService : YAppServiceBase
    {
        private readonly IRepository<DesiredLocationJob> locationJobRepository;
        public DesiredLocationJobAppService(IRepository<DesiredLocationJob> locationJobRepository
        )
        {
            this.locationJobRepository = locationJobRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_DesiredLocationJob)]
        public virtual async Task<PagedResultDto<LocationJobDto>> GetAll(LocationJobFilterDto input)
        {

            var query = locationJobRepository.GetAll()
                .Include(p => p.JobSeeker)
                .Include(p => p.Cities)
                 .WhereIf(input.CityId != null, p => p.CityId == input.CityId)
                 .WhereIf(input.JobSeekerId != null, p => p.JobSeekerId == input.JobSeekerId);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<LocationJobDto>(
                totalCount,
                entities.Select(p => p.MapTo<LocationJobDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditDesiredLocationJobDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditDesiredLocationJobDto();
            if (id == null)
            {
                return model;
            }

            var entity = await locationJobRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_DesiredLocationJob)]
        public async Task CreateOrUpdate(CreateOrEditDesiredLocationJobDto input)
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
        //[AbpAuthorize(PermissionNames.AdminPage_DesiredLocationJob)]
        public async Task Delete(EntityDto<int> input)
        {
            await locationJobRepository.DeleteAsync(p => p.Id == input.Id);

        }
        protected virtual async Task UpdateAsync(CreateOrEditDesiredLocationJobDto input)
        {
            var entity = await locationJobRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await locationJobRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditDesiredLocationJobDto input)
        {

            var entity = ObjectMapper.Map<DesiredLocationJob>(input);

            await locationJobRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<DesiredLocationJob> ApplySorting(IQueryable<DesiredLocationJob> query, LocationJobFilterDto input)
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

        protected virtual IQueryable<DesiredLocationJob> ApplyPaging(IQueryable<DesiredLocationJob> query, LocationJobFilterDto input)
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
