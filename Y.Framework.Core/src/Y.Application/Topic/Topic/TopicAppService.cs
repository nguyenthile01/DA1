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
using Microsoft.EntityFrameworkCore;
using Y.Authorization;
using Y.Core;
using Y.Dto;

namespace Y.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class TopicAppService : YAppServiceBase, ITopicAppService

    {
        private readonly IRepository<Topic> postRepository;
        public TopicAppService(IRepository<Topic> postRepository
        )
        {
            this.postRepository = postRepository;
        }

        #region Admin
        [AbpAuthorize(PermissionNames.AdminPage_Topic)]
        public virtual async Task<PagedResultDto<object>> GetAll(TopicFilterDto input)
        {

            var query = postRepository.GetAll()
                .Include(p => p.TopicCategory)
                .WhereIf(input.Id != null, p => p.Id == input.Id)
                .WhereIf(input.TopicCategoryId != null, p => p.TopicCategoryId == input.TopicCategoryId);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<object>(
                totalCount,
                entities.Select(p => new
                {
                    CategoryName = p.TopicCategory?.Name,
                    p.Id,
                    p.Name,
                    p.CreationTime,
                    p.IsActive,
                    p.DisplayOrder
                })
                    .ToList()
            );
        }

        [AbpAuthorize(PermissionNames.AdminPage_Topic)]
        public async Task CreateOrUpdate(CreateOrEditTopicDto input)
        {
            input.GetDefaultTranslation();
            await SetDefaultSeoValue(input);
            if (input.Id != 0)
            {
                await UpdateAsync(input);
            }
            else
            {
                await CreateUserAsync(input);
            }
        }

        protected virtual async Task UpdateAsync(CreateOrEditTopicDto input)
        {
            var entity = await postRepository.GetAll()
                .Include(p => p.Translations)
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            entity.Translations.Clear();
            ObjectMapper.Map(input, entity);
            await postRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditTopicDto input)
        {

            var entity = ObjectMapper.Map<Topic>(input);

            await postRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        protected async Task SetDefaultSeoValue(CreateOrEditTopicDto input)
        {
            if (input.Translations.HasData())
                input.Translations.ForEach(p =>
                {
                    if (p.MetaTitle.IsNullOrEmpty()) p.MetaTitle = p.Name;
                    if (p.SeoSlug.IsNullOrEmpty()) p.SeoSlug = p.Name.ToSlug();
                });
            if (input.MetaTitle.IsNullOrEmpty())
                input.MetaTitle = input.Name;
            if (input.SeoSlug.IsNullOrEmpty())
                input.SeoSlug = input.Name.ToSlug();
        }

        [AbpAuthorize(PermissionNames.AdminPage_Topic)]
        public async Task Delete(EntityDto<int> input)
        {
            await postRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(PermissionNames.AdminPage_Topic)]
        public async Task<CreateOrEditTopicDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditTopicDto();
            var translations = await InitTranslationsAsync();
            if (id == null)
            {
                model.Translations = translations;
                return model;
            }

            var entity = await postRepository.GetAll()
                .Include(p => p.Translations)
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            model.Translations = model.Translations
                .Concat(translations)
                .DistinctBy(p => p.Language)
                .ToList();
            return model;
        }
        protected async Task<List<TopicTranslationDto>> InitTranslationsAsync()
        {
            return LanguageManager.GetLanguages()
                .Select(p => new TopicTranslationDto()
                {
                    Language = p.Name
                }).ToList();
        }

        protected virtual IQueryable<Topic> ApplySorting(IQueryable<Topic> query, TopicFilterDto input)
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

        protected virtual IQueryable<Topic> ApplyPaging(IQueryable<Topic> query, TopicFilterDto input)
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

        //[AbpAuthorize(PermissionNames.AdminPage_Topic)]
        public async Task<object> GetAllTopicForDropdown()
        {
            var model = await postRepository
                .GetAll()
                .Select(p => new
                {
                    Value = p.Id,
                    Label = p.Name
                })
                .ToListAsync();
            return model;
        }
        #endregion

        #region UI

        public async Task<TopicDto> GetUi(int id)
        {
            var entity = await postRepository.GetAll()
                 .Include(p => p.Translations)
                 .FirstOrDefaultAsync(p => p.Id == id);
            return entity.MapTo<TopicDto>();
        }

        #endregion
    }
}

