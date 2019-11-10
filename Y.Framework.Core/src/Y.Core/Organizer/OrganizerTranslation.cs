//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Domain.Entities;
//using Abp.Domain.Entities.Auditing;
//using Y.Authorization.Users;

//namespace Y.Core
//{
//    public class OrganizerTranslation : SeoBaseEntity<int, User>, IEntityTranslation<Organizer>, IEntity, ISeoEntity
//    {
//        [MaxLength(EntityLength.Name)]
//        public string Name { get; set; }
//        [MaxLength(EntityLength.ShortDescription)]
//        public string ShortDescription { get; set; }

//        [MaxLength(EntityLength.LanguageCode)]
//        public string Language { get; set; }
//        public Organizer Core { get; set; }
//        public int CoreId { get; set; }
//    }
//}
