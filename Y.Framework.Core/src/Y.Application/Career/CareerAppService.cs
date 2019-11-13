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
    public class CareerAppService : YAppServiceBase
    {
        private readonly IRepository<Career> careerRepository;
        public CareerAppService(IRepository<Career> careerRepository
        )
        {
            this.careerRepository = careerRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Career)]
        public virtual async Task<PagedResultDto<CareerDto>> GetAll(CareerFilterDto input)
        {

            var query = careerRepository.GetAll()
                 .WhereIf(input.Id != null, p => p.Id == input.Id);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<CareerDto>(
                totalCount,
                entities.Select(p => p.MapTo<CareerDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditCareerDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditCareerDto();
            if (id == null)
            {
                return model;
            }

            var entity = await careerRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Career)]
        public async Task CreateOrUpdate(CreateOrEditCareerDto input)
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
        //[AbpAuthorize(PermissionNames.AdminPage_Career)]
        public async Task Delete(EntityDto<int> input)
        {
            await careerRepository.DeleteAsync(p => p.Id == input.Id);

        }
        protected virtual async Task UpdateAsync(CreateOrEditCareerDto input)
        {
            var entity = await careerRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await careerRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditCareerDto input)
        {

            var entity = ObjectMapper.Map<Career>(input);

            await careerRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<Career> ApplySorting(IQueryable<Career> query, CareerFilterDto input)
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

        protected virtual IQueryable<Career> ApplyPaging(IQueryable<Career> query, CareerFilterDto input)
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
