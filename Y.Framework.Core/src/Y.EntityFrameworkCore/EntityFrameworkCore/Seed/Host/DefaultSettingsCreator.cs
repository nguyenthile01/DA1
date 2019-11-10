using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using Y.Core;

namespace Y.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly YDbContext _context;

        public DefaultSettingsCreator(YDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            // Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "silverghost1906@gmail.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "Admin");

            // Languages

            // Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en");

            AddSettingIfNotExists(EmailSettingNames.Smtp.EnableSsl, "False");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Host, "smtp.gmail.com");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Password, "svkiallsweojrnyc");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Port, "587");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UserName, "silverghost1906@gmail.com");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Domain, "smtp.gmail.com");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UseDefaultCredentials, "False");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Domain, "smtp.gmail.com");
            //App domain
            //App
            AddSettingIfNotExists(SettingKey.App_BaseUrl, "http://localhost:21021/");
            AddSettingIfNotExists(SettingKey.App_FrontEndBaseUrl, "http://localhost:8080/");
            //Picture
            AddSettingIfNotExists(SettingKey.Picture_MaximumImageSize, "1920");
            //Vnpay
            AddSettingIfNotExists(SettingKey.VnPay_Url, "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html");
            AddSettingIfNotExists(SettingKey.VnPay_TmnCode, "TICKET01");
            AddSettingIfNotExists(SettingKey.VnPay_HashSecret, "IIMOTWJZVEGYXMGKJMXSREVTYTRPGESN");
            AddSettingIfNotExists(SettingKey.VnPay_ViReturnUrl, "http://Sample.vn/payment/paymentResult");
            AddSettingIfNotExists(SettingKey.VnPay_EnReturnUrl, "http://Sample.vn/payment/paymentResult");
            //EPay
            AddSettingIfNotExists(SettingKey.EPay_Url, "http://sanbox.megapay.vn:2710");
            AddSettingIfNotExists(SettingKey.EPay_EncodeKey, "rf8whwaejNhJiQG2bsFubSzccfRc/iRYyGUn6SPmT6y/L7A2XABbu9y4GvCoSTOTpvJykFi6b1G0crU8et2O0Q==");
            AddSettingIfNotExists(SettingKey.EPay_MerchantId, "EPAY000001");
            AddSettingIfNotExists(SettingKey.EPay_ViReturnUrl, "http://staging.Sample.vn/payment/paymentEpayResult");
            AddSettingIfNotExists(SettingKey.EPay_EnReturnUrl, "http://staging.Sample.vn/payment/paymentEpayResult");
            AddSettingIfNotExists(SettingKey.EPay_NotiUrl, "http://backend.staging.Sample.vn/checkout/epaycallback");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}
