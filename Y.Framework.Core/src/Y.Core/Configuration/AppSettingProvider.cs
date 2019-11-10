using System.Collections.Generic;
using Abp.Configuration;
using Y.Core;

namespace Y.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(SettingKey.App_BaseUrl, "http://localhost:21021/", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.App_FrontEndBaseUrl, "http://localhost:8080/", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.VnPay_Url, "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.VnPay_TmnCode, "LKCN0001", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.VnPay_HashSecret, "QMCMDRDVJBSFEFYZTTSXHBNUTVXMNJAX", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.VnPay_ViReturnUrl,"http://Sample.vn/payment/paymentResult", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.VnPay_EnReturnUrl, "http://Sample.vn/payment/paymentResult", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.EPay_Url, "http://sanbox.megapay.vn:2710", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.EPay_EncodeKey, "rf8whwaejNhJiQG2bsFubSzccfRc/iRYyGUn6SPmT6y/L7A2XABbu9y4GvCoSTOTpvJykFi6b1G0crU8et2O0Q==", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.EPay_MerchantId, "EPAY000001", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.EPay_ViReturnUrl,"http://staging.Sample.vn/payment/paymentEpayResult", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.EPay_EnReturnUrl, "http://staging.Sample.vn/payment/paymentEpayResult", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.EPay_NotiUrl, "http://backend.staging.Sample.vn/checkout/epaycallback", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.Picture_MaximumImageSize, "1920", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
            };
        }
    }
}
