using Microsoft.EntityFrameworkCore;
using NOOS_API.Contracts;
using NOOS_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;  // getting an object of ApplicationDb Context that has instructions to interact with the db | object name is _db
        //create a contstructor. initializing done in the constructor
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;               // dependency injection
        }
        
        
        public async Task<bool> Create(Product entity)
        {
            await _db.Products.AddAsync(entity);
            return await Save();     // calling my own function
        }

        public async Task<bool> Delete(Product entity)
        {
            _db.Products.Remove(entity);  // not await applicable
            return await Save();
        }

        public async Task<IList<Product>> FindAll()
        {
            var products = await _db.Products.ToListAsync(); // property has an 's' at the end | class is Product property is Products
            return products;
        }

        public async Task<Product> FindById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            return product;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;         // a boolean relatives to value
        }

        public async Task<bool> Update(Product entity)
        {
            _db.Products.Update(entity);  // not await applicable
            return await Save();
        }
    }
}
