using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity: class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
            ISpecification<TEntity> spec, bool isGetCount = false)
        {
            var q = inputQuery;

            if (spec.Criteria != null)
                q = q.Where(spec.Criteria);

            if (!isGetCount)
            {
                if (spec.OrderBy != null)
                    q = q.OrderBy(spec.OrderBy);
                else if (spec.OrderByDescending != null)
                    q = q.OrderByDescending(spec.OrderByDescending);

                if (spec.IsPagingEnabled)
                    q = q.Skip(spec.Skip).Take(spec.Take);
            }

            q = spec.Includes.Aggregate(q, 
                (current, include) => current.Include(include));

            return q;
        }

        public static IQueryable<TEntity> GetQuery(DbContext context,
            ISpecification<TEntity> spec, bool isGetCount = false)
        {
            var q = context.Set<TEntity>().AsQueryable();
            return GetQuery(q, spec, isGetCount);
        }

    }
}
