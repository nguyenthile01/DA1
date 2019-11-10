using System.ComponentModel.DataAnnotations;

namespace Y.Core
{
    public class Currency : BaseAuditedEntity
    {

        [Required]
        [MaxLength(EntityLength.Name)]
        [Display(Name = "Tên tiền tệ")]
        public string Name { get; set; }

        [Required]
        [MaxLength(EntityLength.Phone)]
        [Display(Name = "Mã")]
        public string CurrencyCode { get; set; }

        [Required]
        [Display(Name = "Tỉ lệ chuyển đổi")]
        public decimal Rate { get; set; }

        [MaxLength(EntityLength.Phone)]
        public string DisplayLocale { get; set; }

        [MaxLength(EntityLength.Phone)]
        public string CustomFormatting { get; set; }

        public int DisplayPrecision { get; set; } = 0;

        public bool IsDefault { get; set; } = false;

        public bool SymbolOnLeft { get; set; } = true;

        [MaxLength(2)]
        public string DecimalSeparator { get; set; } = ".";

        public bool SpaceBetweenAmountAndSymbol { get; set; } = false;

        [MaxLength(2)]
        public string ThousandsSeparator { get; set; } = ",";
    }
}