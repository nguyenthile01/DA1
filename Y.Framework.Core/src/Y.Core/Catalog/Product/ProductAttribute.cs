using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Y.Core
{
    public class ProductAttribute : BaseAuditedEntity, IMultiLingualEntity<ProductAttributeTranslation>
    {
        public int AvatarId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        public ICollection<ProductAttributeTranslation> Translations { get; set; }
    }
}