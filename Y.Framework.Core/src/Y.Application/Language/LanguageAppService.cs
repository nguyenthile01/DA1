using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using Abp.UI;
using AutoMapper;
using Y.Authorization;
using Y.Core;
using Y.Dto;
using Y.Services;

namespace Y.Roles
{
    public class LanguageAppService : YAppServiceBase
    {
        private readonly IApplicationLanguageManager applicationLanguageManager;
        private readonly IApplicationLanguageTextManager applicationLanguageTextManager;
        private readonly IRepository<ApplicationLanguage> languageRepository;
        private readonly ICacheManager cacheManager;

        public LanguageAppService(
            IApplicationLanguageManager applicationLanguageManager,
            IApplicationLanguageTextManager applicationLanguageTextManager,
            IRepository<ApplicationLanguage> languageRepository,
            ICacheManager cacheManager)
        {
            this.applicationLanguageManager = applicationLanguageManager;
            this.languageRepository = languageRepository;
            this.applicationLanguageTextManager = applicationLanguageTextManager;
            this.cacheManager = cacheManager;
        }
        #region UI
        public async Task<GetLanguagesOutput> GetLanguages()
        {
            var languages = (await applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).OrderBy(l => l.DisplayName);
            var defaultLanguage = await applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);

            return new GetLanguagesOutput(
                ObjectMapper.Map<List<ApplicationLanguageListDto>>(languages),
                defaultLanguage?.Name
            );
        }

        public async Task SetDefaultLanguage(SetDefaultLanguageInput input)
        {
            await applicationLanguageManager.SetDefaultLanguageAsync(
                AbpSession.TenantId,
               CultureHelper.GetCultureInfoByChecking(input.Name).Name
                );
        }

        public async Task<PagedResultDto<LanguageTextListDto>> GetLanguageTexts(GetLanguageTextsInput input)
        {
            return await cacheManager.GetCache("Language").Get($"{input.BaseLanguageName}-{input.SourceName}-{input.TargetLanguageName}",
                async () =>
                {
                    //Logger.Info("Languages GetLanguageTexts");
                    /* Note: This method is used by SPA without paging, MPA with paging.
             * So, it can both usable with paging or not */

                    //Normalize base language name
                    if (input.BaseLanguageName.IsNullOrEmpty())
                    {
                        var defaultLanguage = await applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
                        if (defaultLanguage == null)
                        {
                            defaultLanguage = (await applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).FirstOrDefault();
                            if (defaultLanguage == null)
                            {
                                throw new Exception("No language found in the application!");
                            }
                        }

                        input.BaseLanguageName = defaultLanguage.Name;
                    }

                    var source = LocalizationManager.GetSource(input.SourceName);
                    var baseCulture = CultureInfo.GetCultureInfo(input.BaseLanguageName);
                    var targetCulture = CultureInfo.GetCultureInfo(input.TargetLanguageName);

                    var languageTexts = source
                        .GetAllStrings()
                        .Select(localizedString => new LanguageTextListDto
                        {
                            Key = localizedString.Name,
                            BaseValue = applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, baseCulture, localizedString.Name),
                            TargetValue = applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, targetCulture, localizedString.Name, false)
                        })
                        .AsQueryable();

                    //Filters
                    if (input.TargetValueFilter == "EMPTY")
                    {
                        languageTexts = languageTexts.Where(s => s.TargetValue.IsNullOrEmpty());
                    }

                    if (!input.FilterText.IsNullOrEmpty())
                    {
                        languageTexts = languageTexts.Where(
                            l => (l.Key != null && l.Key.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                                 (l.BaseValue != null && l.BaseValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                                 (l.TargetValue != null && l.TargetValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            );
                    }

                    var totalCount = languageTexts.Count();

                    //Ordering
                    if (!input.Sorting.IsNullOrEmpty())
                    {
                        languageTexts = languageTexts.OrderBy(input.Sorting);
                    }

                    //Paging
                    if (input.SkipCount > 0)
                    {
                        languageTexts = languageTexts.Skip(input.SkipCount);
                    }

                    if (input.MaxResultCount > 0)
                    {
                        languageTexts = languageTexts.Take(input.MaxResultCount);
                    }

                    return new PagedResultDto<LanguageTextListDto>(
                        totalCount,
                        languageTexts.ToList()
                        );
                });
        }

        public async Task UpdateLanguageText(UpdateLanguageTextInput input)
        {
            var culture = CultureHelper.GetCultureInfoByChecking(input.LanguageName);
            var source = LocalizationManager.GetSource(input.SourceName);
            await applicationLanguageTextManager.UpdateStringAsync(AbpSession.TenantId, source.Name, culture, input.Key, input.Value);
        }

        public async Task<object> UpdateMultiLanguageText(List<UpdateLanguageTextInput> input)
        {
            if (input.HasData())
            {
                var languages = (await applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId))
                                                                .OrderBy(l => l.DisplayName);
                foreach (var item in input)
                {
                    /* do not use input.LanguageName to get culture, all language names will get from db */
                    foreach (var l in languages)
                    {
                        var culture = CultureHelper.GetCultureInfoByChecking(l.Name);
                        var source = LocalizationManager.GetSource(item.SourceName);
                        await applicationLanguageTextManager.UpdateStringAsync(AbpSession.TenantId, source.Name, culture, item.Key, item.Value);
                    }
                }
            }
            return input;
        }


        #endregion




        #region Admin

        #region Translate
        [AbpAuthorize(PermissionNames.AdminPage_Translate)]
        public async Task<PagedResultDto<LanguageTextListDto>> GetLanguageTextAdmin(GetLanguageTextsInput input)
        {
            /* Note: This method is used by SPA without paging, MPA with paging.
             * So, it can both usable with paging or not */

            //Normalize base language name
            if (input.BaseLanguageName.IsNullOrEmpty())
            {
                var defaultLanguage = await applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
                if (defaultLanguage == null)
                {
                    defaultLanguage = (await applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).FirstOrDefault();
                    if (defaultLanguage == null)
                    {
                        throw new Exception("No language found in the application!");
                    }
                }

                input.BaseLanguageName = defaultLanguage.Name;
            }

            var source = LocalizationManager.GetSource(input.SourceName);
            var baseCulture = CultureInfo.GetCultureInfo(input.BaseLanguageName);
            var targetCulture = CultureInfo.GetCultureInfo(input.TargetLanguageName);

            var languageTexts = source
                .GetAllStrings()
                .Select(localizedString => new LanguageTextListDto
                {
                    Key = localizedString.Name,
                    BaseValue = applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, baseCulture, localizedString.Name),
                    TargetValue = applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, targetCulture, localizedString.Name, false)
                })
                .AsQueryable();

            //Filters
            if (input.TargetValueFilter == "EMPTY")
            {
                languageTexts = languageTexts.Where(s => s.TargetValue.IsNullOrEmpty());
            }

            if (!input.FilterText.IsNullOrEmpty())
            {
                languageTexts = languageTexts.Where(
                    l => (l.Key != null && l.Key.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                         (l.BaseValue != null && l.BaseValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                         (l.TargetValue != null && l.TargetValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    );
            }

            var totalCount = languageTexts.Count();

            //Ordering
            if (!input.Sorting.IsNullOrEmpty())
            {
                languageTexts = languageTexts.OrderBy(input.Sorting);
            }

            //Paging
            if (input.SkipCount > 0)
            {
                languageTexts = languageTexts.Skip(input.SkipCount);
            }

            if (input.MaxResultCount > 0)
            {
                languageTexts = languageTexts.Take(input.MaxResultCount);
            }

            return new PagedResultDto<LanguageTextListDto>(
                totalCount,
                languageTexts.ToList()
                );
        }

       [AbpAuthorize(PermissionNames.AdminPage_Translate)]
        public async Task<List<UpdateLanguageTextInput>> GetForEdit(string key = "")
        {
            if (string.IsNullOrEmpty(key))
                return null;

            var langs = LanguageManager.GetLanguages()
                  .Select(p => p.Name).ToList();
            var model = new List<UpdateLanguageTextInput>();
            var source = LocalizationManager.GetSource("Y");

            var keyDefault = source
                .GetAllStrings()
                .Where(p => p.Name == key)
                .Select(p => p.Name)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(keyDefault))
                throw new UserFriendlyException("Không tìm thấy key");


            foreach (var item in langs)
            {
                model.Add(new UpdateLanguageTextInput
                {
                    Key = keyDefault,
                    SourceName = "Y",
                    LanguageName = item,
                    Value = applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, "Y", CultureInfo.GetCultureInfo(item), key)
                }
                );
            }

            return model;
        }

        [AbpAuthorize(PermissionNames.AdminPage_Translate)]
        public async Task UpdateLanguageTextForAdmin(List<UpdateLanguageTextInput> input)
        {
            foreach (var item in input)
            {
                await UpdateLanguageText(item);
            }
        }
        #endregion

        #region Language
        [AbpAuthorize(PermissionNames.AdminPage_Language)]
        public async Task<ApplicationLanguageEditDto> GetLanguageForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var language = await languageRepository.GetAsync(input.Id.Value);
                if (language == null)
                    throw new Exception("No language found in the application!");
                return ObjectMapper.Map<ApplicationLanguageEditDto>(language);
            }

            return new ApplicationLanguageEditDto();
        }

       [AbpAuthorize(PermissionNames.AdminPage_Language)]
        public async Task<object> GetAllLanguageForDropdown()
        {

            var model = CultureHelper
                     .AllCultures
                     .Select(p => new
                     {
                         value = p.TwoLetterISOLanguageName,
                         label = p.EnglishName
                     })
                     .DistinctBy(p => p.value)
                     .ToList();
            return model;
        }

        [AbpAuthorize(PermissionNames.AdminPage_Language)]
        public async Task CreateOrUpdateLanguage(ApplicationLanguageEditDto input)
        {
            if (input.Id != null)
            {
                await UpdateLanguageAsync(input);
            }
            else
            {
                await CreateLanguageAsync(input);
            }
        }
       // [AbpAuthorize(PermissionNames.AdminPage_Language)]
        protected virtual async Task CreateLanguageAsync(ApplicationLanguageEditDto input)
        {
            if (AbpSession.MultiTenancySide != MultiTenancySides.Host)
            {
                throw new UserFriendlyException(L("TenantsCannotCreateLanguage"));
            }
            var culture = CultureHelper.GetCultureInfoByChecking(input.Name);
            await CheckLanguageIfAlreadyExists(culture.Name);

            input.Icon = FamFamFamFlagsHelper
                        .FlagClassNames
                        .Where(p => FamFamFamFlagsHelper.GetCountryCode(p) == input.Name)
                        .Select(p => p)
                        .FirstOrDefault();

            await applicationLanguageManager.AddAsync(
                new ApplicationLanguage(
                    AbpSession.TenantId,
                    culture.Name,
                    culture.DisplayName,
                    input.Icon,
                    !input.IsEnabled
                )


            );
        }

       // [AbpAuthorize(PermissionNames.AdminPage_Language)]
        protected virtual async Task UpdateLanguageAsync(ApplicationLanguageEditDto input)
        {
            if (input.Id == null)
                throw new UserFriendlyException(L("Không tìm thấy entity"));

            var culture = CultureHelper.GetCultureInfoByChecking(input.Name);

            await CheckLanguageIfAlreadyExists(culture.Name, input.Id.Value);

            input.Icon = FamFamFamFlagsHelper
                        .FlagClassNames
                        .Where(p => FamFamFamFlagsHelper.GetCountryCode(p) == input.Name)
                        .Select(p => p)
                        .FirstOrDefault();

            var language = await languageRepository.GetAsync(input.Id.Value);

            language.Name = culture.Name;
            language.DisplayName = culture.DisplayName;
            language.Icon = input.Icon;
            language.IsDisabled = !input.IsEnabled;

            await applicationLanguageManager.UpdateAsync(AbpSession.TenantId, language);
        }

        //public async Task CreateOrUpdateLanguage(CreateOrUpdateLanguageInput input)
        //{
        //    if (input.Language.Id.HasValue)
        //    {
        //        await UpdateLanguageAsync(input);
        //    }
        //    else
        //    {
        //        await CreateLanguageAsync(input);
        //    }
        //}
        //protected virtual async Task CreateLanguageAsync(CreateOrUpdateLanguageInput input)
        //{
        //    if (AbpSession.MultiTenancySide != MultiTenancySides.Host)
        //    {
        //        throw new UserFriendlyException(L("TenantsCannotCreateLanguage"));
        //    }

        //    var culture = CultureHelper.GetCultureInfoByChecking(input.Language.Name);
        //    await CheckLanguageIfAlreadyExists(culture.Name);

        //    await applicationLanguageManager.AddAsync(
        //        new ApplicationLanguage(
        //            AbpSession.TenantId,
        //            culture.Name,
        //            culture.DisplayName,
        //            input.Language.Icon
        //        )
        //        {
        //            IsDisabled = !input.Language.IsEnabled
        //        }
        //    );
        //}

        //protected virtual async Task UpdateLanguageAsync(CreateOrUpdateLanguageInput input)
        //{
        //    Debug.Assert(input.Language.Id != null, "input.Language.Id != null");

        //    var culture = CultureHelper.GetCultureInfoByChecking(input.Language.Name);

        //    await CheckLanguageIfAlreadyExists(culture.Name, input.Language.Id.Value);

        //    var language = await languageRepository.GetAsync(input.Language.Id.Value);

        //    language.Name = culture.Name;
        //    language.DisplayName = culture.DisplayName;
        //    language.Icon = input.Language.Icon;
        //    language.IsDisabled = !input.Language.IsEnabled;

        //    await applicationLanguageManager.UpdateAsync(AbpSession.TenantId, language);
        //}

        private async Task CheckLanguageIfAlreadyExists(string languageName, int? expectedId = null)
        {
            var existingLanguage = (await applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId))
                .FirstOrDefault(l => l.Name == languageName);

            if (existingLanguage == null)
            {
                return;
            }

            if (expectedId != null && existingLanguage.Id == expectedId.Value)
            {
                return;
            }

            throw new UserFriendlyException(L("Ngôn ngữ này đã tồn tại"));
        }

        [AbpAuthorize(PermissionNames.AdminPage_Language)]
        public async Task DeleteLanguage(EntityDto input)
        {
            var language = await languageRepository.GetAsync(input.Id);
            await applicationLanguageManager.RemoveAsync(AbpSession.TenantId, language.Name);
        }

        [AbpAuthorize(PermissionNames.AdminPage_Language)]
        public async Task<object> GetLanguageAdmin()
        {
            var languages = (await applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).OrderBy(l => l.DisplayName);

            return languages;
    }
        #endregion

        #endregion
    }
}

