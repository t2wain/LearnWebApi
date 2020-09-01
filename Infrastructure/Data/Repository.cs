using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class Repository : IRepository
    {
        private readonly AdventureWorksContext _context;

        public Repository(AdventureWorksContext context)
        {
            this._context = context;
        }

        #region Customer

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await this._context.Customer.FindAsync(id);
        }

        public async Task<IReadOnlyList<Customer>> GetCustomersAsync()
        {
            return await this._context.Customer.ToListAsync();
        }

        #endregion

        #region Product

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await this.ProductQtry(id).SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await this.ProductQtry().ToListAsync();
        }

        protected IQueryable<Product> ProductQtry(int? id = null)
        {
            var q = this._context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .AsQueryable();
            if (id.HasValue)
                q = q.Where(p => p.ProductId == id.Value);
            return q;
        }

        #endregion
    }
}
