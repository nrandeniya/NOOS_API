using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOOS_API.Data
{
    [Table("Sellers")]
    public partial class Seller
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessURL { get; set; }

        public string? Industry { get; set; }
        //public string? Location { get; set; }
        //public string? PhoneNo { get; set; }


        public virtual IList<Subscription> Subscriptions { get; set; }
    }
}