using E_Commerce.Domain.Comman;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastracture.Data;
using E_Commerce.Infrastracture.Specfications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture.Repositories
{
    internal class GenericRepository<TEntity, Tkey>(StoreDbContext dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default) => await dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecfications<TEntity, Tkey> spec, CancellationToken ct)
        {
            var query = SpecficationEvaluator.CreateQuery(dbContext.Set<TEntity>(), spec);
            return await query.ToListAsync(ct);
        }
        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default) => await dbContext.Set<TEntity>().FindAsync(id, ct);

        public async Task<TEntity?> GetByIdAsync(ISpecfications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            var query = SpecficationEvaluator.CreateQuery(dbContext.Set<TEntity>(), spec);
            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<int> CountAsync(ISpecfications<TEntity, Tkey> spec, CancellationToken ct)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            query =  SpecficationEvaluator.CreateQuery(query, spec);
            return await query.CountAsync(ct);
        }
    }
}
