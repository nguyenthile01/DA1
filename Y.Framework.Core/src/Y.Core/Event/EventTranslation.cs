//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Domain.Entities;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Y.Authorization.Users;

//namespace Y.Core
//{
//    public class EventTranslation : SeoBaseEntity<int, User>, IEntityTranslation<Event>, IEntity
//    {
//        [MaxLength(EntityLength.Name)]
//        [Required]
//        public string Name { get; set; }
//        [MaxLength(EntityLength.ShortDescription)]
//        public string ShortDescription { get; set; }
//        public string FullDescription { get; set; }
//        [MaxLength(EntityLength.Address)]
//        [Required]
//        public string Location { get; set; }
//        [MaxLength(EntityLength.ShortDescription)]
//        [Required]
//        public string EventTerm { get; set; }


//        public virtual Event Core { get; set; }
//        public int CoreId { get; set; }
//        public string Language { get; set; }
//    }
//}
