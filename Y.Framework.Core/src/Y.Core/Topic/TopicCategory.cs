using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Y.Core
{
    public class TopicCategory : BaseCategory
    {
        public virtual ICollection<Topic> Topics { get; set; }
    }
}