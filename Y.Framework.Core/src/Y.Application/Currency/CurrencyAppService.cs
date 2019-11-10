using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Y.Core;
using Y.Dto;

namespace Y.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class CurrencyAppService : YAppServiceBase, ICurrencyAppService
    {
        private readonly IRepository<Currency> repository;
        private readonly ICacheManager cacheManager;

        public CurrencyAppService(IRepository<Currency> repository, ICacheManager cacheManager)
        {
            this.repository = repository;
            this.cacheManager = cacheManager;
        }

        public virtual async Task<List<CurrencyDto>> GetAllUi()
        {
            //var query = repository.GetAll();

            //var entities = await query.ToListAsync();

            //return entities.Select(p => p.MapTo<CurrencyDto>()).ToList();
            return await cacheManager.GetCache("Currency").Get($"CurrencyGetAllUi-{CurrentLanguageName}", async () =>
            {
                //Logger.Info("Currency GetAllUi");
                var query = repository.GetAll();

                var entities = await query.ToListAsync();

                return entities.Select(p => p.MapTo<CurrencyDto>()).ToList();
            });
        }

        #region Admin

        public virtual async Task<PagedResultDto<CurrencyDto>> GetAll(CurrencyFilterDto input)
        {
            var query = repository
                        .GetAll()
                        .Where(p => !p.IsDeleted)
                        .WhereIf(input.Name != null, p => p.Name == input.Name)
                        .WhereIf(input.CurrencyCode != null, p => p.CurrencyCode == input.CurrencyCode);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<CurrencyDto>(
                totalCount,
                entities.Select(p => p.MapTo<CurrencyDto>())
                    .ToList()
            );
        }

        protected virtual IQueryable<Currency> ApplySorting(IQueryable<Currency> query, CurrencyFilterDto input)
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

        protected virtual IQueryable<Currency> ApplyPaging(IQueryable<Currency> query, CurrencyFilterDto input)
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

        public async Task<CreateOrEditCurrencyDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditCurrencyDto();
            if (id == null)
            {
                model.DisplayPrecision = 0;
                model.IsDefault = false;
                model.SymbolOnLeft = true;
                model.DecimalSeparator = ".";
                model.SpaceBetweenAmountAndSymbol = false;
                model.ThousandsSeparator = ",";
                return model;
            }

            var entity = await repository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);

            return model;
        }

        public async Task CreateOrUpdate(CreateOrEditCurrencyDto input)
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

        protected virtual async Task UpdateAsync(CreateOrEditCurrencyDto input)
        {
            var entity = await repository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await repository.UpdateAsync(entity);
        }

        //[AbpAuthorize(AppPermissions.Pages_Administration_Users_Create)]
        protected virtual async Task CreateAsync(CreateOrEditCurrencyDto input)
        {

            var entity = ObjectMapper.Map<Currency>(input);

            await repository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task Delete(EntityDto<int> input)
        {
            await repository.DeleteAsync(p => p.Id == input.Id);
        }

        #endregion
    }
}

