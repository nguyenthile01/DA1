using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    public static class EntityLength
    {
        public const int ShortDescription = 1000;
        public const int Name = 500;
        public const int FirstName = 50;
        public const int LastName = 200;
        public const int Address = 300;
        public const int Email = 100;
        public const int Phone = 50;
        public const int Url = 100;
        public const int TicketNumber = 13;
        public const int LanguageCode = 2;
        public const int UserCookie = 20;
    }
    public static class SettingKey
    {
        public const string App_BaseUrl = "App_BaseUrl";
        public const string App_FrontEndBaseUrl = "App_FrontEndBaseUrl";
        public const string Picture_MaximumImageSize = "Picture_MaximumImageSize";

        public const string VnPay_Url = "VnPay_Url";
        public const string VnPay_TmnCode = "VnPay_TmnCode";
        public const string VnPay_HashSecret = "VnPay_HashSecret";
        public const string VnPay_EnReturnUrl = "VnPay_EnReturnUrl";
        public const string VnPay_ViReturnUrl = "VnPay_ViReturnUrl";

        //epay settings
        public const string EPay_Url = "EPay_Url";
        public const string EPay_MerchantId= "EPay_MerchantId";
        public const string EPay_EncodeKey = "EPay_EncodeKey";
        public const string EPay_EnReturnUrl = "EPay_EnReturnUrl";
        public const string EPay_ViReturnUrl = "EPay_ViReturnUrl";
        public const string EPay_NotiUrl = "EPay_NotiUrl";
    }
    public static class CacheKey
    {
        public const string Picture_GetAll = "Picture_GetAll";
        public const string Picture_Get = "Picture_Get";
    }

    //public static class TicketStage
    //{
    //    public const string EarlyBird = "Early bird";
    //    public const string StageOne = "Giai đoạn 1";
    //    public const string StageTwo = "Giai đoạn 2";
    //}
    public static class TicketPriceStatus
    {
        public const string SoldOut = "Sold out";
        public const string ComingSoon = "Coming Soon";
        public const string Active = "Active";
    }
    public static class ContactType
    {
        public const string GetInTouch = "Get in touch";

    }
    public static class SourceDefault
    {
        public const string Sample = "Sample.vn";

    }

    public static class LanguageTypes
    {
        public const string vi = "vi";
        public const string en = "en";
    }

    public static class EventType
    {
        public const string Events = "Events";
        public const string Gaia = "Gaia";
    }

    public static class PaymentGateWay
    {
        public const string VNPAY = "VNPAY";
        public const string EPAY = "EPAY";
    }
    public static class CategoryTypes
    {
        public const string NewsCategory = "News";
        public const string BlogCategory = "Blog";
        public const string TopicCategory = "Topic";
        public const string EventCategory = "Event";
    }

    public static class SurchargeTypes
    {
        public const string Vat = "VAT";
        public const string PromotionCode = "Coupon";
        public const string OnlinePayment = "Online payment";
        public const string DiscountOnCheckoutTotal = "Discount on checkout total";
        public const string DiscountOnPackageQuantity = "Discount on package quantity";
        public const string DiscountOnTotalQuantity = "Discount on total quantity";
    }

    public static class DiscountAmountType
    {
        public const string Percent = "Percent";
        public const string FixedAmount = "FixedAmount";
    }

    public static class SellSourceAction
    {
        public const string NotGetSampleOffline = "NotGetSampleOffline";
    }
}
