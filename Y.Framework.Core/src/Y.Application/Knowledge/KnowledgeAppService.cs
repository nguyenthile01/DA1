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
    public class KnowledgeAppService : YAppServiceBase
    {
        private readonly IRepository<Knowledge> knowledgeRepository;
        public KnowledgeAppService(IRepository<Knowledge> knowledgeRepository
        )
        {
            this.knowledgeRepository = knowledgeRepository;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Knowledge)]
        public virtual async Task<PagedResultDto<KnowledgeDto>> GetAll(KnowledgeFilterDto input)
        {

            var query = knowledgeRepository.GetAll()
                 .WhereIf(input.Id != null, p => p.Id == input.Id);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<KnowledgeDto>(
                totalCount,
                entities.Select(p => p.MapTo<KnowledgeDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditKnowledgeDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditKnowledgeDto();
            if (id == null)
            {
                return model;
            }

            var entity = await knowledgeRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Knowledge)]
        public async Task CreateOrUpdate(CreateOrEditKnowledgeDto input)
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
        //[AbpAuthorize(PermissionNames.AdminPage_Knowledge)]
        public async Task Delete(EntityDto<int> input)
        {
            await knowledgeRepository.DeleteAsync(p => p.Id == input.Id);

        }
        protected virtual async Task UpdateAsync(CreateOrEditKnowledgeDto input)
        {
            var entity = await knowledgeRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await knowledgeRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditKnowledgeDto input)
        {

            var entity = ObjectMapper.Map<Knowledge>(input);

            await knowledgeRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<Knowledge> ApplySorting(IQueryable<Knowledge> query, KnowledgeFilterDto input)
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

        protected virtual IQueryable<Knowledge> ApplyPaging(IQueryable<Knowledge> query, KnowledgeFilterDto input)
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
