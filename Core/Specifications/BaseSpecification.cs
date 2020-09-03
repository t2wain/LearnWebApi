using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() 
        {
            Includes = new List<Expression<Func<T, object>>>();
        }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; }
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public bool IsCountEnabled { get; set; }
        public bool IsPagingEnabled { get; set; } = true;
        public int Take { get; set; } = 50;
        public int Skip { get; set; }
        protected void ApplyPaging(int pageSize, int pageIndex)
        {
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
    }
}
