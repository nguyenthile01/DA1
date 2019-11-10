using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Net.Mail;
using Y.Core;

namespace Y.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    //todo: udpate permission
    public class SettingAppService : ISettingAppService
    {
        private readonly ISettingManager _settingManager;
        private readonly IEmailSender _emailSender;

        public SettingAppService(ISettingManager settingManager,
            IEmailSender emailSender)
        {
            _settingManager = settingManager;
            _emailSender = emailSender;
        }
        //public async Task<List<SettingDetail>> GetMobileSettings()
        //{
        //    return new List<SettingDetail>()
        //    {
        //            new SettingDetail()
        //            {
        //                Name = "Sync inspection slot interval",
        //                SettingKey = SettingKey.Mobile_SyncInspectionIntervalSecond,
        //                Value = _settingManager.GetSettingValue<int>(SettingKey.Mobile_SyncInspectionIntervalSecond),
        //                ValueType = nameof(Int32)
        //            },
        //        new SettingDetail()
        //        {
        //            Name = "Sync geo tracking interval",
        //            SettingKey = SettingKey.Mobile_SyncGeoIntervalSecond,
        //            Value = _settingManager.GetSettingValue<int>(SettingKey.Mobile_SyncGeoIntervalSecond),
        //            ValueType = nameof(Int32)
        //        }
        //    };
        //}
        public List<SettingDto> GetAll()
        {
            #region Get All Settings
            return new List<SettingDto>
            {
                new SettingDto()
                {
                    GroupName = "Email",
                    Settings = new List<SettingDetail>()
                    {
                        new SettingDetail()
                        {
                            Name = "Email from address",
                            SettingKey = EmailSettingNames.DefaultFromAddress,
                            Value = _settingManager.GetSettingValue(EmailSettingNames.DefaultFromAddress)
                        },
                        new SettingDetail()
                        {
                            Name = "Email from display name",
                            SettingKey = EmailSettingNames.DefaultFromDisplayName,
                            Value = _settingManager.GetSettingValue(EmailSettingNames.DefaultFromDisplayName)
                        },
                        new SettingDetail()
                        {
                            Name = "Email SMTP host",
                            SettingKey = EmailSettingNames.Smtp.Host,
                            Value = _settingManager.GetSettingValue(EmailSettingNames.Smtp.Host)
                        },
                        new SettingDetail()
                        {
                            Name = "Email SMTP port",
                            SettingKey = EmailSettingNames.Smtp.Port,
                            Value = _settingManager.GetSettingValue(EmailSettingNames.Smtp.Port)
                        },
                        new SettingDetail()
                        {
                            Name = "Email SMTP username",
                            SettingKey = EmailSettingNames.Smtp.UserName,
                            Value = _settingManager.GetSettingValue(EmailSettingNames.Smtp.UserName)
                        },
                        new SettingDetail()
                        {
                            Name = "Email SMTP domain",
                            SettingKey = EmailSettingNames.Smtp.Domain,
                            Value = _settingManager.GetSettingValue(EmailSettingNames.Smtp.Domain)
                        },

                        new SettingDetail()
                        {
                            Name = "Email SMTP enable SSL",
                            SettingKey = EmailSettingNames.Smtp.EnableSsl,
                            ValueType =  nameof(Boolean),
                            Value = bool.Parse(_settingManager.GetSettingValue(EmailSettingNames.Smtp.EnableSsl))
                        },
                        //new SettingDetail()
                        //{
                        //    Name = "Email SMTP use default credential",
                        //    SettingKey = EmailSettingNames.Smtp.UseDefaultCredentials,
                        //    ValueType =  nameof(Boolean),
                        //    Value = bool.Parse(_settingManager.GetSettingValue(EmailSettingNames.Smtp.UseDefaultCredentials))
                        //},
                        new SettingDetail()
                        {
                            Name = "Email SMTP password",
                            SettingKey = EmailSettingNames.Smtp.Password,
                            Value = _settingManager.GetSettingValue(EmailSettingNames.Smtp.Password)
                        }
                    }
                },
                new SettingDto()
                {
                    GroupName = "System",
                    Settings = new List<SettingDetail>()
                    {
                        new SettingDetail()
                        {
                            Name = "Base Url",
                            SettingKey = SettingKey.App_BaseUrl,
                            Value = _settingManager.GetSettingValue(SettingKey.App_BaseUrl),
                        },
                        new SettingDetail()
                        {
                            Name = "Picture Maximum Image Size",
                            SettingKey = SettingKey.Picture_MaximumImageSize,
                            Value = _settingManager.GetSettingValue(SettingKey.Picture_MaximumImageSize),
                            ValueType = nameof(Int32)
                        },

                    }
                },
                  new SettingDto()
                {
                    GroupName = "VnPay Setting",
                    Settings = new List<SettingDetail>()
                    {
                        new SettingDetail()
                        {
                            Name = "Url",
                            SettingKey = SettingKey.VnPay_Url,
                            Value = _settingManager.GetSettingValue(SettingKey.VnPay_Url),
                        },
                        new SettingDetail()
                        {
                            Name = "TmnCode",
                            SettingKey = SettingKey.VnPay_TmnCode,
                            Value = _settingManager.GetSettingValue(SettingKey.VnPay_TmnCode),
                        },
                         new SettingDetail()
                        {
                            Name = "HashSecret",
                            SettingKey = SettingKey.VnPay_HashSecret,
                            Value = _settingManager.GetSettingValue(SettingKey.VnPay_HashSecret),
                        },
                            new SettingDetail()
                        {
                            Name = "Vi Return Url",
                            SettingKey = SettingKey.VnPay_ViReturnUrl,
                            Value = _settingManager.GetSettingValue(SettingKey.VnPay_ViReturnUrl),
                        },
                               new SettingDetail()
                        {
                            Name = "En Return Url",
                            SettingKey = SettingKey.VnPay_EnReturnUrl,
                            Value = _settingManager.GetSettingValue(SettingKey.VnPay_EnReturnUrl),
                        },

                    }
                },
                   new SettingDto()
                {
                    GroupName = "EPay Setting",
                    Settings = new List<SettingDetail>()
                    {
                        new SettingDetail()
                        {
                            Name = "Url",
                            SettingKey = SettingKey.EPay_Url,
                            Value = _settingManager.GetSettingValue(SettingKey.EPay_Url),
                        },
                        new SettingDetail()
                        {
                            Name = "EncodeKey",
                            SettingKey = SettingKey.EPay_EncodeKey,
                            Value = _settingManager.GetSettingValue(SettingKey.EPay_EncodeKey),
                        },
                        new SettingDetail()
                        {
                            Name = "MerchantId",
                            SettingKey = SettingKey.EPay_MerchantId,
                            Value = _settingManager.GetSettingValue(SettingKey.EPay_MerchantId),
                        },
                        new SettingDetail()
                        {
                            Name = "NotiUrl",
                            SettingKey = SettingKey.EPay_NotiUrl,
                            Value = _settingManager.GetSettingValue(SettingKey.EPay_NotiUrl),
                        },
                        new SettingDetail()
                        {
                            Name = "Vi ReturnUrl",
                            SettingKey = SettingKey.EPay_ViReturnUrl,
                            Value = _settingManager.GetSettingValue(SettingKey.EPay_ViReturnUrl),
                        },
                        new SettingDetail()
                        {
                            Name = "En Return Url",
                            SettingKey = SettingKey.EPay_EnReturnUrl,
                            Value = _settingManager.GetSettingValue(SettingKey.EPay_EnReturnUrl),
                        },
                    }
                },
            };
            #endregion
        }

        public async Task<List<SettingDto>> Update(List<SettingDto> input)
        {
            var settings = input
                .SelectMany(p => p.Settings)
                .ToList();
            settings.ForEach(p => { _settingManager.ChangeSettingForApplication(p.SettingKey, p.Value?.ToString()); });
            return GetAll();
        }
    }
}
