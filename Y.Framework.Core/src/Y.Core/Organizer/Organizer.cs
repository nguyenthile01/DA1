//using Abp.Domain.Entities;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Y.Core
//{
//    [Table("Organizers")]
//    public class Organizer : BaseAuditedEntity, IMultiLingualEntity<OrganizerTranslation>, ISeoEntity
//    //, IMultiLingualEntity<OrganizerTranslation>
//    {
//        #region Localize

//        [MaxLength(EntityLength.Name)]
//        public string Name { get; set; }
//        [MaxLength(EntityLength.ShortDescription)]
//        public string ShortDescription { get; set; }

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

//        [MaxLength(EntityLength.Name)]
//        public string Type { get; set; }
            

//        [MaxLength(EntityLength.Phone)]
//        public string Phone { get; set; }

//        [MaxLength(EntityLength.Email)]
//        public string Email { get; set; }
//        [MaxLength(EntityLength.Url)]
//        public string Facebook { get; set; }
//        [MaxLength(EntityLength.Url)]
//        public string Youtube { get; set; }
//        [Display(Name = "Ảnh đại diện")]
//        public int AvatarId { get; set; }
//        [Display(Name = "Hiển thị ngoài trang chủ?")]
//        public bool ShowOnHomePage { get; set; }
//        [MaxLength(EntityLength.Url)]
//        public string LogoUrl { get; set; }
//        [MaxLength(EntityLength.Url)]
//        public string Website { get; set; }

//        public virtual ICollection<EventOrganizer> EventOrganizers { get; set; }
//        public ICollection<OrganizerTranslation> Translations { get; set; }
//    }
//}
