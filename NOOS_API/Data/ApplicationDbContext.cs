using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NOOS_API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        DbSet<Subscription> Subscriptions { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Seller> Sellers { get; set; }
        DbSet<Buyer> Buyers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
