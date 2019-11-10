using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Y.Core
{
    [Serializable]
    [Table("AbpLanguageTexts")]
    public class ApplicationLanguageText : AuditedEntity<long>
    {
    }
}
