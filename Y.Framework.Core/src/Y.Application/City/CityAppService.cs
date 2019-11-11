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
    public class CityAppService : YAppServiceBase
    {
        private readonly IRepository<City> cityRepository;
        public CityAppService(IRepository<City> cityRepository
        )
        {
            this.cityRepository = cityRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_City)]
        public virtual async Task<PagedResultDto<CityDto>> GetAll(CityFilterDto input)
        {

            var query = cityRepository.GetAll()
                 .WhereIf(input.Id != null, p => p.Id == input.Id);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<CityDto>(
                totalCount,
                entities.Select(p => p.MapTo<CityDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditCityDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditCityDto();
            if (id == null)
            {
                return model;
            }

            var entity = await cityRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_City)]
        public async Task CreateOrUpdate(CreateOrEditCityDto input)
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
        //[AbpAuthorize(PermissionNames.AdminPage_City)]
        public async Task Delete(EntityDto<int> input)
        {
            await cityRepository.DeleteAsync(p => p.Id == input.Id);

        }
        protected virtual async Task UpdateAsync(CreateOrEditCityDto input)
        {
            var entity = await cityRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await cityRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditCityDto input)
        {

            var entity = ObjectMapper.Map<City>(input);

            await cityRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<City> ApplySorting(IQueryable<City> query, CityFilterDto input)
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

        protected virtual IQueryable<City> ApplyPaging(IQueryable<City> query, CityFilterDto input)
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
