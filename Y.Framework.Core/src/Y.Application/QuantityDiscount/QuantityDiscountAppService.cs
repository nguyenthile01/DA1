using System;
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
using Abp.Net.Mail;
using Abp.Timing;
using Abp.UI;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Y.Authorization;
using Y.Core;
using Y.Dto;

namespace Y.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class QuantityDiscountAppService : YAppServiceBase, IQuantityDiscountAppService
    {
        private readonly IRepository<QuantityDiscount> quantityDiscountRepository;

        public QuantityDiscountAppService(IRepository<QuantityDiscount> quantityDiscountRepository)
        {
            this.quantityDiscountRepository = quantityDiscountRepository;
        }

        #region Admin
        public virtual async Task<PagedResultDto<QuantityDiscountDto>> GetAll(QuantityDiscountFilterDto input)
        {
            var query = quantityDiscountRepository
                        .GetAll()
                        .Where(p => !p.IsDeleted)
                        .WhereIf(input.DiscountPercent != null, p => p.DiscountPercent == input.DiscountPercent)
                        .WhereIf(input.MinQuantity != null, p => p.MinQuantity == input.MinQuantity);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<QuantityDiscountDto>(
                totalCount,
                entities.Select(p => p.MapTo<QuantityDiscountDto>())
                    .ToList()
            );
        }

        protected virtual IQueryable<QuantityDiscount> ApplySorting(IQueryable<QuantityDiscount> query, QuantityDiscountFilterDto input)
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

        protected virtual IQueryable<QuantityDiscount> ApplyPaging(IQueryable<QuantityDiscount> query, QuantityDiscountFilterDto input)
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

        public async Task<QuantityDiscountCreateOrEditDto> GetForEdit(int? id)
        {
            var model = new QuantityDiscountCreateOrEditDto();
            if (id == null)
            {
                model.DiscountPercent = 0;
                model.MinQuantity = 0;
                model.SelectedEvents = "";
                return model;
            }

            var entity = await quantityDiscountRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);

            return model;
        }

        public async Task CreateOrUpdate(QuantityDiscountCreateOrEditDto input)
        {
            if (input.Id != 0)
            {
                await UpdateAsync(input);
            }
            else
            {
                await CreateAsync(input);
            }
        }

        protected virtual async Task UpdateAsync(QuantityDiscountCreateOrEditDto input)
        {
            var entity = await quantityDiscountRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await quantityDiscountRepository.UpdateAsync(entity);
        }


        protected virtual async Task CreateAsync(QuantityDiscountCreateOrEditDto input)
        {

            var entity = ObjectMapper.Map<QuantityDiscount>(input);

            await quantityDiscountRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        //[AbpAuthorize(PermissionNames.AdminPage_Post)]
        public async Task Delete(EntityDto<int> input)
        {
            await quantityDiscountRepository.DeleteAsync(p => p.Id == input.Id);
        }
        #endregion
    }
}

