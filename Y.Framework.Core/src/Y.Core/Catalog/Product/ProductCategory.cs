using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Y.Core
{
    [Table("ProductCategories")]
    public class ProductCategory : BaseCategory
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}