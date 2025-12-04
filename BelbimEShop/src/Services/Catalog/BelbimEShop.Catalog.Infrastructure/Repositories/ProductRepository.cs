using BelbimEShop.Catalog.Domain.Aggregates;
using BelbimEShop.Catalog.Domain.Repositories;
using BelbimEShop.Catalog.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;

        }

        public async Task DeleteAsync(long id)
        {

            var entity = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
           return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(long id)
        {
           return await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            return await _context.Products.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            return entity;


        }
    }
}
