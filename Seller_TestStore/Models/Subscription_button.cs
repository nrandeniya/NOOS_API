using System;
using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Models
{
    public class Subscription_button
    {
        [Key]
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
      //  public int BuyerId { get; set; }  //might have to replace with 'BuyerEmail'
        public DateTime Timestamp { get; set; }
        public decimal OriginalPrice { get; set; }



    }
}
