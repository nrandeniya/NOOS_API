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
        public int BuyerId { get; set; }
        public DateTime Timestamp { get; set; }
        public double OriginalPrice { get; set; }

    }
}
