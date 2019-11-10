using System.ComponentModel.DataAnnotations;

namespace Y.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}