using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
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
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class PostAppService : YAppServiceBase, IPostAppService

    {
        private readonly IRepository<Post> postRepository;
        public PostAppService(IRepository<Post> postRepository
        )
        {
            this.postRepository = postRepository;
        }

        #region Admin
        [AbpAuthorize(PermissionNames.AdminPage_Post)]
        public virtual async Task<PagedResultDto<PostDto>> GetAll(PostFilterDto input)
        {

            var query = postRepository.GetAll()
                .Include(p => p.Translations)
                .Include(p => p.Category)
                 .WhereIf(input.Id != null, p => p.Id == input.Id)
                .WhereIf(input.CategoryId != null, p => p.CategoryId == input.CategoryId);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<PostDto>(
                totalCount,
                entities.Select(p => p.MapTo<PostDto>())
                    .ToList()
            );
        }

        [AbpAuthorize(PermissionNames.AdminPage_Post)]
        public async Task CreateOrUpdate(CreateOrEditPostDto input)
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

       
        protected virtual async Task UpdateAsync(CreateOrEditPostDto input)
        {
            var entity = await postRepository.GetAll()
                .Include(p => p.Translations)
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            entity.Translations.Clear();
            ObjectMapper.Map(input, entity);
            await postRepository.UpdateAsync(entity);
        }

       
        protected virtual async Task CreateUserAsync(CreateOrEditPostDto input)
        {

            var entity = ObjectMapper.Map<Post>(input);

            await postRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        protected async Task SetDefaultSeoValue(CreateOrEditPostDto input)
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

        [AbpAuthorize(PermissionNames.AdminPage_Post)]
        public async Task Delete(EntityDto<int> input)
        {
            await postRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(PermissionNames.AdminPage_Post)]
        public async Task<CreateOrEditPostDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditPostDto();
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
        protected async Task<List<PostTranslationDto>> InitTranslationsAsync()
        {
            return LanguageManager.GetLanguages()
                .Select(p => new PostTranslationDto()
                {
                    Language = p.Name
                }).ToList();
        }

        protected virtual IQueryable<Post> ApplySorting(IQueryable<Post> query, PostFilterDto input)
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

        protected virtual IQueryable<Post> ApplyPaging(IQueryable<Post> query, PostFilterDto input)
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
        public async Task<object> GetAllPostForDropdown()
        {
            var model = await postRepository
                .GetAll()
                .Include(p => p.Translations)
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

        public async Task<PostDto> GetUi(int id)
        {
            var entity = await postRepository.GetAll()
                 .Include(p => p.Translations)
                 .Where(p => p.IsActive)
                 .FirstOrDefaultAsync(p => p.Id == id);

            if (entity == null)
                throw new EntityNotFoundException(L("Data not found"));
            var data = entity.MapTo<PostDto>();

            GetPictureUrls(data);
            return data;
        }
        public virtual async Task<PagedResultDto<PostDto>> GetAllUi(PostFilterDto input)
        {
            var query = postRepository.GetAll()
                .Include(p => p.Translations)
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .WhereIf(input.CategoryId != null, p => p.CategoryId == input.CategoryId);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query
                            .OrderByDescending(p => p.DisplayOrder)
                            .ToListAsync();

            var result = entities.Select(p => p.MapTo<PostDto>())
                    .ToList();

            //foreach (var item in result)
            //{
            //    GetPictureUrls(item);
            //}
            GetPictureUrls(result);

            var data = new PagedResultDto<PostDto>(
                totalCount,
                result
            );
            //foreach (var item in data.Items)
            //{
            //    item.AvatarUrl = PictureManager.GetPictureUrl(item.AvatarId, 600);
            //    item.BannerUrl = PictureManager.GetPictureUrl(item.BannerId, 1920);
            //}
            return data;
        }

        public virtual async Task<List<PostDto>> GetRecentPosts(int? postCategoryId, int takePost = 5)
        {
            var query = postRepository.GetAll()
                          .Include(p => p.Translations)
                          .WhereIf(postCategoryId != null, p => p.CategoryId == postCategoryId);

            var entities = query
                            .OrderByDescending(p => p.CreationTime)
                            .Take(takePost)
                            .ToList()
                            .Select(p => p.MapTo<PostDto>())
                            .ToList();

            foreach (var item in entities)
            {
                item.AvatarUrl = PictureManager.GetPictureUrl(item.AvatarId, 730);
                item.BannerUrl = PictureManager.GetPictureUrl(item.BannerId, 1920);
            }
            return entities;
        }

        public virtual async Task<List<PostDto>> GetRelatedPosts(int postId, int takePost = 5)
        {
            var relatedPost = new List<PostDto>();
            var post = await postRepository
                            .GetAll()
                            .FirstOrDefaultAsync(p => p.Id == postId);
            if (post != null)
            {
                if (post.Category != null)
                {
                    relatedPost = postRepository
                                    .GetAll()
                                    .Include(p => p.Translations)
                                    .Where(p => p.CategoryId == post.CategoryId && p.Id != post.Id)
                                    .Distinct()
                                    .TakeRandom(takePost)
                                    .ToList()
                                    .Select(p => p.MapTo<PostDto>())
                                    .ToList();
                }

                if (relatedPost.Count < takePost)
                {
                    var relatedPostIds = relatedPost.Select(p => p.Id).ToList();

                    var remainingPost = postRepository
                                       .GetAll()
                                       .Include(p => p.Translations)
                                       .Where(p => !relatedPostIds.Contains(p.Id)
                                                     && p.Id != postId)
                                       .TakeRandom(takePost - relatedPostIds.Count)
                                       .Select(p => p.MapTo<PostDto>())
                                       .ToList();

                    if (remainingPost.HasData())
                    {
                        relatedPost.AddRange(remainingPost);
                    }
                }
            }
            else
            {
                relatedPost = postRepository
                                .GetAll()
                                .Include(p => p.Translations)
                                .TakeRandom(takePost)
                                .ToList()
                                .Select(p => p.MapTo<PostDto>())
                                .ToList();
            }

            foreach (var item in relatedPost)
            {
                item.AvatarUrl = PictureManager.GetPictureUrl(item.AvatarId, 600);
                item.BannerUrl = PictureManager.GetPictureUrl(item.BannerId, 1920);
            }

            return relatedPost;
        }

        #endregion
    }
}

