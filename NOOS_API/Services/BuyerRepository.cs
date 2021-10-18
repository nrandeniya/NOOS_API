using Microsoft.EntityFrameworkCore;
using NOOS_API.Contracts;
using NOOS_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.Services
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly ApplicationDbContext _db;  // getting an object of ApplicationDb Context that has instructions to interact with the db | object name is _db
        //create a contstructor. initializing done in the constructor
        public BuyerRepository(ApplicationDbContext db)
        {
            _db = db;               // dependency injection
        }
        
        public async Task<bool> Create(Buyer entity)
        {
            await _db.Buyers.AddAsync(entity);
            return await Save();     // calling my own function
        }

        public async Task<bool> Delete(Buyer entity)
        {
            _db.Buyers.Remove(entity);  // not await applicable
            return await Save();
        }

        public async Task<IList<Buyer>> FindAll()
        {
            var buyers = await _db.Buyers.ToListAsync(); // property has an 's' at the end | class is Buyer property is Buyers
            return buyers;
        }

        public async Task<Buyer> FindById(int id)
        {
            var buyer = await _db.Buyers.FindAsync(id);
            return buyer;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;         // a boolean relatives to value
        }

        public async Task<bool> Update(Buyer entity)
        {
            _db.Buyers.Update(entity);  // not await applicable
            return await Save();
        }
    }
}
