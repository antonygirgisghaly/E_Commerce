using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture.Specfications
{
    internal class SpecficationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecfications<TEntity, Tkey> spec) where TEntity : Domain.Comman.BaseEntity<Tkey>
        {
            var query = inputQuery;
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.IncludeExpressions.Any())
            {
                query = spec.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));
            }
            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if(spec.IsPaginated)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            return query;
        }
    }
}
