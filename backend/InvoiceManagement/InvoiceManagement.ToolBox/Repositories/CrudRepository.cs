using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.ToolBox.Repositories
{
    public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
    {

        protected DbContext _dbContext;
        protected DbSet<TEntity> _entities;

        public CrudRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<TEntity>();
        }

        public bool Any(Func<TEntity, bool> predicate)
        {
            return _entities.Any(predicate);
        }

        public bool Delete(TEntity entity)
        {
            _entities.Remove(entity);
            return _dbContext.SaveChanges() == 1;
        }

        public IEnumerable<TEntity> FindMany(Func<TEntity, bool>? predicate = null)
        {
            if(predicate == null)
            {
                return _entities;
            }
            return _entities.Where(predicate);
        }

        public TEntity? FindOne(Func<TEntity, bool> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public TEntity? FindOne(params object[] ids)
        {
            return _entities.Find(ids);
        }

        public TEntity Save(TEntity entity)
        {
            TEntity newEntity = _entities.Add(entity).Entity;
            _dbContext.SaveChanges();
            return newEntity;
        }

        public bool Update(TEntity entity)
        {
            _entities.Update(entity);
            return _dbContext.SaveChanges() == 1;
        }
        
        public void AddRange(IEnumerable<TEntity> entities)
        {
             _entities.AddRange(entities);
             _dbContext.SaveChanges();
        }
    }
}