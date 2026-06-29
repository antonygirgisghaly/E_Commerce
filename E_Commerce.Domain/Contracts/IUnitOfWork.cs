using E_Commerce.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct);
        IGenericRepository<TEntity,Tkey> GetGenericRepository<TEntity,Tkey>() where TEntity : BaseEntity<Tkey>;
    }
}
