//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Y.Core
//{

//    [Table("ShoppingCartItems")]
//    public class ShoppingCartItem : BaseAuditedHardDeleteEntity
//    {
//        public string UserId { get; set; }
//        public int TicketTypeId { get; set; }
//        //public int TicketPriceId { get; set; }
//        public int Quantity { get; set; }
//        public string Type { get; set; }
//        [ForeignKey(nameof(TicketTypeId))]
//        public virtual TicketType TicketType { get; set; }
//        [MaxLength(EntityLength.Name)]
//        public string Source { get; set; }
//        //[ForeignKey(nameof(TicketPriceId))]
//        //public virtual TicketPrice TicketPrice { get; set; }
//    }
//}
