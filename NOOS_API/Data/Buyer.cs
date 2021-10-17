using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOOS_API.Data
{
    [Table("Buyers")]
    public partial class Buyer
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }

        public virtual IList<Subscription> Subscriptions { get; set; }
    }
}