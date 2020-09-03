using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            this._context = context;
        }

        public async Task<T> GetById(ISpecification<T> spec)
        {
            var q = ApplySpecficiation(spec);
            return await q.SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> spec)
        {
            var q = ApplySpecficiation(spec);
            return await q.ToListAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await ApplySpecficiation(spec).CountAsync();
        }

        protected IQueryable<T> ApplySpecficiation(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(this._context, spec);
        }

    }
}
