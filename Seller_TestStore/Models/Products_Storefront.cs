using System;
using System.Collections.Generic;
using System.Text;

namespace SubscriptionManagement.Models
{
    public class Products_Storefront
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? Sale_Price { get; set; }
        public bool IsOn_sale { get; set; }
        public string? Permalink { get; set; }
        public string PhotoPath { get; set; }

    }
}
