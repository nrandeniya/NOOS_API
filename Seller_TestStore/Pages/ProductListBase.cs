using Microsoft.AspNetCore.Components;
using SubscriptionManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller_TestStore.Pages
{
    public class ProductListBase : ComponentBase

    {
        public IEnumerable<Products_Storefront> Products { get; set; }

        protected override Task OnInitializedAsync()
        {
            LoadProducts();
            return base.OnInitializedAsync();
        }
        private void LoadProducts()
        {
            Products_Storefront p1 = new Products_Storefront
            {
                Id = 1234,
                ProductName = "Subaru Forester LHS headlight",
                Price = 250.00M,
                Sale_Price = 220.00M,
                PhotoPath= "Images/subaru-forester-s3-sh-headlight.JPG"
            };

            Products_Storefront p2 = new Products_Storefront
            {
                Id = 2222,
                ProductName = "BMW E60 2004-2009 RHS rear door panel",
                Price = 350.00M,
                Sale_Price = 342.00M,
                PhotoPath = "Images/bmw-e60-doorshell-475.JPG"
            };

            Products_Storefront p3 = new Products_Storefront
            {
                Id = 3333,
                ProductName = "Subaru intake snorkel - EJ253 2007-2010",
                Price = 80.00M,
                PhotoPath= "Images/ej25-intake-snorkal.jpg"

            };

            //Products_Storefront p4 = new Products_Storefront
            //{
            //    Id = 4444,
            //    ProductName = "Subaru WRX/ Forester XT manual transmission TY758VGZAA",
            //    Price = 800.00M,
            //    PhotoPath = "Images/wrx-sti-manual-tranny.jpg"

            //};


            Products = new List<Products_Storefront> { p1, p2, p3};

        }
    }
}
