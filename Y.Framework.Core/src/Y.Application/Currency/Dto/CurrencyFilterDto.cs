namespace Y.Dto
{
    public class CurrencyFilterDto : YPagedAndSortDto
    {
        public string Name { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Rate { get; set; }

        public string DisplayLocale { get; set; }
    }
}