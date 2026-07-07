using E_Commerce.Domain.Comman;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specfications
{
    internal abstract class BaseSpecfication<TEntity, Tkey> : ISpecfications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria {  get; private set; }

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int pageSize, int PageIndex)
        {
            IsPaginated = true;
            Skip = (pageSize - 1) * PageIndex;
            Take = pageSize;
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        protected BaseSpecfication(Expression<Func<TEntity, bool>> condition)
        {
            Criteria = condition;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExpressions.Add(include);
        }
    }
}
