using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    [AutoMap(typeof(Currency))]
    public class CurrencyUpdateDto : BaseUpdateDto
    {
        [Required]
        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }

        [Required]
        [MaxLength(EntityLength.Phone)]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [MaxLength(EntityLength.Phone)]
        public string DisplayLocale { get; set; }

        [MaxLength(EntityLength.Phone)]
        public string CustomFormatting { get; set; }

        public int DisplayPrecision { get; set; } = 0;

        public bool IsDefault { get; set; } = false;

        public bool SymbolOnLeft { get; set; } = true;

        public string DecimalSeparator { get; set; }

        public bool SpaceBetweenAmountAndSymbol { get; set; } = false;

        public string ThousandsSeparator { get; set; }
    }
}