using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.DTOs
{
    public class SubscriptionDTO
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public string BuyerEmail { get; set; }
        public int BuyerId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal OriginalPrice { get; set; }
    }

    public class ButtonTriggerDTO
    {
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }  //might have to replace with 'BuyerEmail'
        public DateTime Timestamp { get; set; }
        public decimal OriginalPrice { get; set; }

    }
}
