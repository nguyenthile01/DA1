

//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Dynamic.Core;
//using System.Threading.Tasks;
//using Abp.Application.Services.Dto;
//using Abp.Authorization;
//using Abp.AutoMapper;
//using Abp.Domain.Repositories;
//using Abp.Extensions;
//using Abp.Linq.Extensions;
//using Abp.Localization;
//using JetBrains.Annotations;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Y.Authorization;
//using Y.Core;
//using Y.Dto;

//namespace Y.Services
//{
//    //[AbpAuthorize(PermissionNames.Pages_Users)]
//    public class PostCategoryAppService : YAppServiceBase, IPostCategoryAppService

//    {
//        private readonly IRepository<PostCategory> postCategoryRepository;
//        public PostCategoryAppService(IRepository<PostCategory> postCategoryRepository
//        )
//        {
//            this.postCategoryRepository = postCategoryRepository;
//        }

//        #region Admin
//        [AbpAuthorize(PermissionNames.AdminPage_PostCategory)]
//        public async Task<object> GetAdminCategoryDropdown()
//        {
//            var model = await postCategoryRepository
//                .GetAll()
//                .Include(p => p.Translations)
//                .Select(p => new
//                {
//                    Value = p.Id,
//                    Label = p.Name
//                })
//                .ToListAsync();
//            return model;
//        }

//       [AbpAuthorize(PermissionNames.AdminPage_PostCategory)]
//        public virtual async Task<PagedResultDto<PostCategoryDto>> GetAll(PostCategoryFilterDto input)
//        {

//            var query = postCategoryRepository.GetAll()
//                    .Include(p => p.Translations)
//                 .WhereIf(input.Id != null, p => p.Id == input.Id);

//            var totalCount = await query.CountAsync();

//            query = ApplySorting(query, input);
//            query = ApplyPaging(query, input);

//            var entities = await query.ToListAsync();

//            return new PagedResultDto<PostCategoryDto>(
//                totalCount,
//                entities.Select(p => p.MapTo<PostCategoryDto>())
//                    .ToList()
//            );
//        }

//        [AbpAuthorize(PermissionNames.AdminPage_PostCategory)]
//        public async Task CreateOrUpdate(CreateOrEditPostCategoryDto input)
//        {
//            input.GetDefaultTranslation();
//            await SetDefaultSeoValue(input);
//            if (input.Id != 0)
//            {
//                await UpdateAsync(input);
//            }
//            else
//            {
//                await CreateUserAsync(input);
//            }
//        }

//        protected virtual async Task UpdateAsync(CreateOrEditPostCategoryDto input)
//        {
//            var entity = await postCategoryRepository.GetAll()
//                .Include(p => p.Translations)
//                .FirstOrDefaultAsync(p => p.Id == input.Id);

//            entity.Translations.Clear();
//            ObjectMapper.Map(input, entity);
//            await postCategoryRepository.UpdateAsync(entity);
//        }

//        protected virtual async Task CreateUserAsync(CreateOrEditPostCategoryDto input)
//        {

//            var entity = ObjectMapper.Map<PostCategory>(input);

//            await postCategoryRepository.InsertAndGetIdAsync(entity);
//            await CurrentUnitOfWork.SaveChangesAsync();
//        }

//        protected async Task SetDefaultSeoValue(CreateOrEditPostCategoryDto input)
//        {
//            if (input.Translations.HasData())
//                input.Translations.ForEach(p =>
//                {
//                    if (p.MetaTitle.IsNullOrEmpty()) p.MetaTitle = p.Name;
//                    if (p.SeoSlug.IsNullOrEmpty()) p.SeoSlug = p.Name.ToSlug();
//                });
//            if (input.MetaTitle.IsNullOrEmpty())
//                input.MetaTitle = input.Name;
//            if (input.SeoSlug.IsNullOrEmpty())
//                input.SeoSlug = input.Name.ToSlug();
//        }

//        [AbpAuthorize(PermissionNames.AdminPage_PostCategory)]
//        public async Task Delete(EntityDto<int> input)
//        {
//            await postCategoryRepository.DeleteAsync(p => p.Id == input.Id);
//        }

//        [AbpAuthorize(PermissionNames.AdminPage_PostCategory)]
//        public async Task<CreateOrEditPostCategoryDto> GetForEdit(int? id)
//        {
//            var model = new CreateOrEditPostCategoryDto();
//            var translations = await InitTranslationsAsync();
//            if (id == null)
//            {
//                model.Translations = translations;
//                return model;
//            }

//            var entity = await postCategoryRepository.GetAll()
//                .Include(p => p.Translations)
//                .FirstOrDefaultAsync(p => p.Id == id);

//            entity.MapTo(model);
//            model.Translations = model.Translations
//                .Concat(translations)
//                .DistinctBy(p => p.Language)
//                .ToList();
//            return model;
//        }
//        protected async Task<List<PostCategoryTranslationDto>> InitTranslationsAsync()
//        {
//            return LanguageManager.GetLanguages()
//                .Select(p => new PostCategoryTranslationDto()
//                {
//                    Language = p.Name
//                }).ToList();
//        }

//        protected virtual IQueryable<PostCategory> ApplySorting(IQueryable<PostCategory> query, PostCategoryFilterDto input)
//        {
//            var sortInput = input as ISortedResultRequest;
//            if (sortInput != null)
//            {
//                if (sortInput.Sorting.IsNotNullOrEmpty())
//                {
//                    return query.OrderBy(sortInput.Sorting);
//                }
//            }

//            if (input is ILimitedResultRequest)
//            {
//                return query.OrderByDescending(e => e.Id);
//            }

//            return query;
//        }

//        protected virtual IQueryable<PostCategory> ApplyPaging(IQueryable<PostCategory> query, PostCategoryFilterDto input)
//        {
//            var pagedInput = input as IPagedResultRequest;
//            if (pagedInput != null)
//            {
//                return query.PageBy(pagedInput);
//            }

//            var limitedInput = input as ILimitedResultRequest;
//            if (limitedInput != null)
//            {
//                return query.Take(limitedInput.MaxResultCount);
//            }

//            return query;
//        }
//        #endregion

//        #region UI

//        public async Task<PostCategoryDto> GetUi(int id)
//        {
//            var entity = await postCategoryRepository
//                             .GetAll()
//                             .Include(p => p.Translations)
//                             .FirstOrDefaultAsync(p => p.Id == id);
//            return entity.MapTo<PostCategoryDto>();
//        }
//        public virtual async Task<List<PostCategoryDto>> GetList()
//        {
//            var query = postCategoryRepository
//                          .GetAll()
//                          .Include(p => p.Translations);                          

//            var entities = query
//                            .OrderByDescending(p => p.CreationTime)                          
//                            .ToList()
//                            .Select(p => p.MapTo<PostCategoryDto>())
//                            .ToList();

//            return entities;
//        }
//        #endregion
//    }
//}

