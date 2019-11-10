using Abp.AutoMapper;
using Y.Core;

namespace Y.Dto
{
    [AutoMapFrom(typeof(Currency))]
    public class CurrencyDto : BaseAuditedDto
    {
        public string Name { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Rate { get; set; }

        public string DisplayLocale { get; set; }

        public string CustomFormatting { get; set; }

        public int DisplayPrecision { get; set; }

        public bool IsDefault { get; set; }

        public bool SymbolOnLeft { get; set; } = true;

        public string DecimalSeparator { get; set; }

        public bool SpaceBetweenAmountAndSymbol { get; set; } = false;

        public string ThousandsSeparator { get; set; }
    }
}