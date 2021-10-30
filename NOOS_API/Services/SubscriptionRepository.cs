using Microsoft.EntityFrameworkCore;
using NOOS_API.Contracts;
using NOOS_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.Services
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _db;  // getting an object of ApplicationDb Context that has instructions to interact with the db | object name is _db
        //create a contstructor. initializing done in the constructor
        public SubscriptionRepository(ApplicationDbContext db)
        {
            _db = db;               // dependency injection
        }
         
        public async Task<bool> Create(Subscription entity)
        {
            try
            {
                await _db.Subscriptions.AddAsync(entity);
                return await Save();     // calling my own function
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        public async Task<bool> Delete(Subscription entity)
        {
            _db.Subscriptions.Remove(entity);  // not await applicable
            return await Save();
        }

        public async Task<IList<Subscription>> FindAll()
        {
            var subscriptions = await _db.Subscriptions.ToListAsync(); // property has an 's' at the end | class is Subscription property is Subscriptions
            return subscriptions;
        }

        public async Task<Subscription> FindById(int id)
        {
            var subscription = await _db.Subscriptions.FindAsync(id);
            return subscription;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;         // a boolean relatives to value
        }

        public async Task<bool> Update(Subscription entity)
        {
            _db.Subscriptions.Update(entity);  // not await applicable
            return await Save();
        }
    }
}
