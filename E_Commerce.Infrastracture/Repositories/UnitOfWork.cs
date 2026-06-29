using E_Commerce.Domain.Comman;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastracture.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture.Repositories
{
    internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _keys = [];
        public IGenericRepository<TEntity, Tkey> GetGenericRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var type = typeof(TEntity).Name;
            if (_keys.TryGetValue(type, out object? value))
                return (IGenericRepository<TEntity,Tkey>) value;
            else 
            {
                var repo = new GenericRepository<TEntity, Tkey>(dbContext);
                _keys[type] = repo;
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct) => await dbContext.SaveChangesAsync(ct);
    }
}
