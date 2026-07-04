using E_Commerce.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface ISpecfications<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    }
}
