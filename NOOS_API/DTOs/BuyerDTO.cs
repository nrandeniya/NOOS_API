using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.DTOs
{
    public class BuyerDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }

        public virtual IList<SubscriptionDTO> Subscriptions { get; set; }
    }
}
