using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Y.Core
{
    public class ProductAttributeTranslation : BaseAuditedEntity, IEntityTranslation<ProductAttribute>, IEntity
    {
        [MaxLength(EntityLength.Name)]
        public string Name { get; set; }
        [MaxLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }
        public string Language { get; set; }
        public ProductAttribute Core { get; set; }
        public int CoreId { get; set; }
    }
}