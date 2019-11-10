using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Y.Authorization.Users;

namespace Y.Core
{
    //public class CategoryTranslation : SeoBaseEntity<int, User>, IEntityTranslation<Category>, IEntity
    //{
    //    [MaxLength(EntityLength.Name)]
    //    [Required]
    //    public string Name { get; set; }
    //    [MaxLength(EntityLength.ShortDescription)]
    //    public string ShortDescription { get; set; }
    //    public string FullDescription { get; set; }
      
    //    public virtual Category Core { get; set; }
    //    public int CoreId { get; set; }
    //    [MaxLength(EntityLength.LanguageCode)]
    //    public string Language { get; set; }
    //}
}
