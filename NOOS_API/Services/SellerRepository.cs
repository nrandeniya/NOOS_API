using Microsoft.EntityFrameworkCore;
using NOOS_API.Contracts;
using NOOS_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.Services
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ApplicationDbContext _db;  // getting an object of ApplicationDb Context that has instructions to interact with the db | object name is _db
        //create a contstructor. initializing done in the constructor
        public SellerRepository(ApplicationDbContext db)
        {
            _db = db;               // dependency injection
        }
         
        public async Task<bool> Create(Seller entity)
        {
           // try
           // {
                await _db.Sellers.AddAsync(entity);
                return await Save();     // calling my own function
           // }
           // catch (Exception e)
           // {

              //  throw e;
           // }
           
        }

        public async Task<bool> Delete(Seller entity)
        {
            _db.Sellers.Remove(entity);  // not await applicable
            return await Save();
        }

      
        public async Task<IList<Seller>> FindAll()
        {
            var sellers = await _db.Sellers.ToListAsync(); // property has an 's' at the end | class is Seller property is Sellers
            return sellers;
        }

        public async Task<Seller> FindById(int id)
        {
            var sellers = await _db.Sellers.FindAsync(id);
            return sellers;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;         // a boolean relatives to value
        }

        public async Task<bool> Update(Seller entity)
        {
            _db.Sellers.Update(entity);  // not await applicable
            return await Save();
        }

    }
}
