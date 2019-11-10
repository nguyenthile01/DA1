using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Runtime.Session;
using Y.Authorization.Users;
using Y.MultiTenancy;
using Y.Services;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Abp.Domain.Entities;
using Y.Core;
using Y.Dto;
using System.Threading;

namespace Y
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class YAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }
        public UserManager UserManager { get; set; }
        public LanguageManager LanguageManager { get; set; }
        public PictureAppService PictureManager { get; set; }
        public ApplicationLanguageTextManager ApplicationLanguageTextManager { get; set; }

        public string CurrentLanguageName {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.Name;
            }
        }

        protected YAppServiceBase()
        {
            LocalizationSourceName = YConsts.LocalizationSourceName;
        }

        protected override string L(string name)
        {
            var data = base.L(name);
            if (data.StartsWith("[") && data.EndsWith("]"))
            {
                Task.Run(() => ApplicationLanguageTextManager.UpdateStringAsync(AbpSession.TenantId,
                    LocalizationSourceName, CultureInfo.CurrentUICulture, name,
                    name));
                data = base.L(name);
            }

            return data;
        }

        protected virtual void GetPictureUrls<T>(List<T> model) where T : class
        {
            if (model.HasData())
                foreach (var item in model)
                {
                    GetPictureUrls(item);
                }
        }

        protected virtual void GetPictureUrls<T>(T model) where T : class
        {
            if (model == null) return;
            var type = typeof(T);
            var specialProperties = type.GetRuntimeProperties()
                .Where(pi => pi.PropertyType == typeof(string)
                             && pi.GetCustomAttributes<PictureUrlAttribute>(true).Any())
                .ToList();
            if (!specialProperties.HasData()) return;
            foreach (var specialProperty in specialProperties)
            {
                var pInfo = typeof(T).GetProperty(specialProperty.Name)
                    .GetCustomAttribute<PictureUrlAttribute>();
                var idProperty = pInfo.From.IsNotNullOrEmpty() ? typeof(T).GetProperty(pInfo.From) : null;
                int size = pInfo.Size;
                if (idProperty != null)
                {
                    var idPropertyValue = idProperty.GetValue(model);
                    if (idPropertyValue != null)
                    {
                        if (idProperty.PropertyType == typeof(int))

                            specialProperty.SetValue(model, PictureManager.GetPictureUrl((int)idPropertyValue, size));
                        else
                            specialProperty.SetValue(model, PictureManager.GetPictureUrls((string)idPropertyValue, size));
                    }
                }
            }
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
