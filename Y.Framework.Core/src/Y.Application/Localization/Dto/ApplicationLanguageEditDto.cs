using System.ComponentModel.DataAnnotations;
using Abp.Localization;

namespace Y.Dto
{
    public class ApplicationLanguageEditDto
    {
        public virtual int? Id { get; set; }

        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength)]
        public virtual string Name { get; set; }

        [StringLength(ApplicationLanguage.MaxIconLength)]
        public virtual string Icon { get; set; }

        public string DisplayName { get; set; }
        /// <summary>
        /// Mapped from Language.IsDisabled with using manual mapping in CustomDtoMapper.cs
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}