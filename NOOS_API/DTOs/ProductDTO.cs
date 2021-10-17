using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public double Sale_Price { get; set; }
        public bool IsOn_sale { get; set; }
        public string Permalink { get; set; }

        public virtual IList<SubscriptionDTO> Subscriptions { get; set; }
    }
}
