using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Y.Core
{
    public interface ISeoEntity
    {
        string SeoSlug { get; set; }
        [MaxLength(150)]
        string MetaTitle { get; set; }
        [MaxLength(500)]
        string MetaDescription { get; set; }
        [MaxLength(500)]
        string CanonicalUrl { get; set; }
    }
}
