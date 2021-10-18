using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? Sale_Price { get; set; }
        public bool IsOn_sale { get; set; }
        public string? Permalink { get; set; }

        public virtual IList<SubscriptionDTO> Subscriptions { get; set; }
    }

    public class ProductCreateDTO  // allows to controll what client enters
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? Sale_Price { get; set; }
        public bool IsOn_sale { get; set; }
        public string? Permalink { get; set; }
    }
}
