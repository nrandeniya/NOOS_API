using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_UI.Models
{
    public class Subscription
    {
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public int BuyerEmail { get; set; }  //might have to replace with 'BuyerEmail'
        public DateTime Timestamp { get; set; }
        public decimal OriginalPrice { get; set; }
    }
}
