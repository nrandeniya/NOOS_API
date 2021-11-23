using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.DTOs
{
    public class SellerDTO
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessURL { get; set; }
        public string? Industry { get; set; }
        public string? PhoneNo { get; set; }

        public virtual IList<SubscriptionDTO> Subscriptions { get; set; }
        public virtual IList<ProductDTO> Products { get; set; }
    }
}
