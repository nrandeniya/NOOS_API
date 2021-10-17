using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOOS_API.Data
{
    [Table("Sellers")]
    public partial class Seller
    {
        public int Id { get; set; }
        public string SellerName { get; set; }
        public string? Category { get; set; }
        public string? Location { get; set; }
        public string? PhoneNo { get; set; }


        public virtual IList<Subscription> Subscriptions { get; set; }
    }
}