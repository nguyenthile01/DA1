//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Abp.Domain.Entities;
//using Abp.Domain.Entities.Auditing;
//using Y.Authorization.Users;

//namespace Y.Core
//{
//    [Table("Events")]
//    public class Event : BaseAuditedEntity, IMultiLingualEntity<EventTranslation>, ISeoEntity
//    {
//        #region Localize

//        [MaxLength(EntityLength.Name)]
//        public string Name { get; set; }
//        [MaxLength(EntityLength.ShortDescription)]
//        public string ShortDescription { get; set; }
//        public string FullDescription { get; set; }
//        [MaxLength(EntityLength.Address)]
//        public string Location { get; set; }
//        [MaxLength(EntityLength.ShortDescription)]
//        public string EventTerm { get; set; }

//        #region SEO

//        [MaxLength(500)]
//        public string SeoSlug { get; set; }
//        [MaxLength(150)]
//        public string MetaTitle { get; set; }
//        [MaxLength(500)]
//        public string MetaDescription { get; set; }
//        [MaxLength(500)]
//        public string CanonicalUrl { get; set; }

//        #endregion

//        #endregion

//        public DateTime StartTime { get; set; }
//        public DateTime EndTime { get; set; }
//        [MaxLength(EntityLength.Url)]
//        public string BannerId { get; set; }
//        [MaxLength(EntityLength.Url)]
//        public string AvatarId { get; set; }

//        [MaxLength(EntityLength.Url)]
//        public string BannerUrl { get; set; }

//        [MaxLength(EntityLength.Url)]
//        public string LogoUrl { get; set; }

//        [MaxLength(EntityLength.Name)]
//        public string FolderTicket{ get; set; }

//        [MaxLength(EntityLength.Name)]
//        public string TicketTemplate { get; set; }


//        [MaxLength(EntityLength.Url)]
//        public string OpenHours { get; set; }

//        [MaxLength(EntityLength.FirstName)]
//        public string Type { get; set; }

//        public bool ShowHomeBanner { get; set; }

//        public bool IsComingSoon { get; set; }
//        public virtual ICollection<TicketBooked> TicketBookeds { get; set; }
//        public virtual ICollection<EventOrganizer> EventOrganizers { get; set; }
//        public virtual ICollection<TicketType> TicketTypes { get; set; } = new HashSet<TicketType>();
//        public virtual ICollection<TicketPrice> TicketPrices { get; set; } = new HashSet<TicketPrice>();
//        public virtual ICollection<EventTranslation> Translations { get; set; }
//    }
//}
