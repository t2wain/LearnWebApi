using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity: class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
            ISpecification<TEntity> spec)
        {
            var q = inputQuery;

            if (spec.Criteria != null)
                q = q.Where(spec.Criteria);

            if (!spec.IsCountEnabled)
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
            ISpecification<TEntity> spec)
        {
            var q = context.Set<TEntity>().AsQueryable();
            return GetQuery(q, spec);
        }

    }
}
