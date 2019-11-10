//using System;
//using System.Globalization;
//using Abp.Application.Services;
//using Abp.Domain.Repositories;
//using Abp.Localization;
//using Abp.Runtime.Session;
//using System.Linq;
//using System.Linq.Dynamic.Core;
//using System.Threading.Tasks;
//using Abp.Application.Services.Dto;
//using Y.Dto;
//using Y.Services;
//using Abp.Extensions;
//using Abp.UI;

//namespace Y.Roles
//{
//    public class LocalizationAppService : YAppServiceBase
//    {
//        private readonly IApplicationLanguageTextManager applicationLanguageTextManager;
//        private readonly IApplicationLanguageManager applicationLanguageManager;
//        private readonly ILocalizationManager localizationManager;
//        public LocalizationAppService(IApplicationLanguageTextManager applicationLanguageTextManager,
//            ILocalizationManager localizationManager
//            , IApplicationLanguageManager applicationLanguageManager)
//        {
//            this.applicationLanguageTextManager = applicationLanguageTextManager;
//            this.localizationManager = localizationManager;
//            this.applicationLanguageManager = applicationLanguageManager;
//        }

//        public async Task UpdateLanguageText(UpdateLanguageTextInput input)
//        {
//            var culture = CultureHelper.GetCultureInfoByChecking(input.LanguageName);
//            var source = LocalizationManager.GetSource(input.SourceName);
//            await applicationLanguageTextManager.UpdateStringAsync(AbpSession.TenantId, source.Name, culture, input.Key, input.Value);
//        }
//        public string GetText(string mytext)
//        {
//            return L(mytext);

//        }

//        public async Task<PagedResultDto<LanguageTextListDto>> GetLanguageTexts(GetLanguageTextsInput input)
//        {
//            /* Note: This method is used by SPA without paging, MPA with paging.
//             * So, it can both usable with paging or not */
//            //Normalize base language name
//            if (input.BaseLanguageName.IsNullOrEmpty())
//            {
//                var defaultLanguage = await applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
//                if (defaultLanguage == null)
//                {
//                    defaultLanguage = (await applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).FirstOrDefault();
//                    if (defaultLanguage == null)
//                    {
//                        throw new UserFriendlyException("No language found in the application!");
//                    }
//                }

//                input.BaseLanguageName = defaultLanguage.Name;
//            }

//            var source = LocalizationManager.GetSource(input.SourceName);
//            var baseCulture = CultureInfo.GetCultureInfo(input.BaseLanguageName);
//            var targetCulture = CultureInfo.GetCultureInfo(input.TargetLanguageName);

//            var languageTexts = source
//                .GetAllStrings()
//                .Select(localizedString => new LanguageTextListDto
//                {
//                    Key = localizedString.Name,
//                    BaseValue = applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, baseCulture, localizedString.Name),
//                    TargetValue = applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, targetCulture, localizedString.Name, false)
//                })
//                .AsQueryable();

//            //Filters
//            if (input.TargetValueFilter == "EMPTY")
//            {
//                languageTexts = languageTexts.Where(s => s.TargetValue.IsNullOrEmpty());
//            }

//            if (!input.FilterText.IsNullOrEmpty())
//            {
//                languageTexts = languageTexts.Where(
//                    l => (l.Key != null && l.Key.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
//                         (l.BaseValue != null && l.BaseValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
//                         (l.TargetValue != null && l.TargetValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0)
//                    );
//            }

//            var totalCount = languageTexts.Count();

//            //Ordering
//            if (!input.Sorting.IsNullOrEmpty())
//            {
//                languageTexts = languageTexts.OrderBy(input.Sorting);
//            }

//            //Paging
//            if (input.SkipCount > 0)
//            {
//                languageTexts = languageTexts.Skip(input.SkipCount);
//            }

//            if (input.MaxResultCount > 0)
//            {
//                languageTexts = languageTexts.Take(input.MaxResultCount);
//            }

//            return new PagedResultDto<LanguageTextListDto>(
//                totalCount,
//                languageTexts.ToList()
//                );
//        }

//        public object GetSources()
//        {
//            var results = localizationManager
//                .GetAllSources()
//                .Where(s => s.GetType() == typeof(MultiTenantLocalizationSource))
//                .Select(s => new
//                {
//                    Value = s.Name,
//                    Text = s.Name
//                })
//                .ToList();

//            return results;
//        }


//        //public object GetLocalizedTexts(string sourceName)
//        //{
//        //    var texts = localizationManager.GetSource(sourceName);

//        //    return texts.GetAllStrings();
//        //}

//        //public async Task UpdateLanguageText(UpdateLanguageTextInput input)
//        //{
//        //    var culture = CultureHelper.GetCultureInfoByChecking(input.LanguageName);
//        //    var source = localizationManager.GetSource(input.SourceName);
//        //    await applicationLanguageTextManager.UpdateStringAsync(AbpSession, source.Name, culture, input.Key, input.Value);
//        //}

//    }
//}

