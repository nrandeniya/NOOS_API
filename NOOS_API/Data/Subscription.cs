using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOOS_API.Data
{
    [Table("Subscriptions")]
    public partial class Subscription
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal OriginalPrice { get; set; }

    }
}