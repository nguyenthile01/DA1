using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Y.Authorization.Users;

namespace Y.Core
{
    public enum ApiExecuteStatus
    {
        Fail = 1,
        Success = 2
    }
    public enum ReceiptStatus
    {
        [Description("Chờ xử lý")]
        Received = 0,
        [Description("Đang xử lý")]
        Processing = 10,
        [Description("Đã thanh toán")]
        Completed = 20,
        [Description("Đã hủy")]
        Canceled = 30,
    }

    public enum PaymentMenthods
    {
        PaymentLater,
        PaymentOnline,
        PaymentOffline
    }

    public enum Sources
    {
        SampleOffline
    }

    public enum EnumRoles
    {
        AdminId = 1,
        ManagerId = 3,
        CheckIn = 4,
        OrderOffline = 5
    }
    public enum PromotionCodeStatus
    {
        NotExist = 1,
        Exprired = 2,
        OutOfStock = 3,
        InValid = 4,
        Valid = 5
    }

    public enum DiscountType
    {
        Coupon = 1,
        PercentOnline = 2,
        PercentOnBill = 3,
        PercentOnPackage = 4,
        PercentOnTotalQuantity = 5,
    }

    public enum TicketBookedError
    {
        NotFound,
        Invalid,
        ExpiredTime,
        IsUsed
    }
}