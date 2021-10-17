using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOOS_API.Data
{
    [Table("Products")]
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public double Sale_Price { get; set; }
        public bool IsOn_sale { get; set; }
        public string Permalink { get; set; }

        public virtual IList<Subscription> Subscriptions { get; set; }


    }
}