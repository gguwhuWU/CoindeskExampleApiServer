using CoindeskExampleApiServer.Protocols.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoindeskExampleApiServer.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="dbContext"></param>
    public class GenericRepository<TEntity>(CoindeskDbContext dbContext) : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly CoindeskDbContext _dbContext = dbContext;

        /// <summary>
        /// 
        /// </summary>
        protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void Create(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task CreateAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void Delete(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public TEntity? Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="entity"></param>
        public void PartialUpdate(IDictionary<string, object?> properties, TEntity entity)
        {
            var entityEntry = _dbContext.Attach(entity);

            foreach (var ky in properties)
            {
                entityEntry.Member(ky.Key).CurrentValue = ky.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void Update(List<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TEntity> FindAll()
        {
            return [.. _dbSet];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Detached(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
