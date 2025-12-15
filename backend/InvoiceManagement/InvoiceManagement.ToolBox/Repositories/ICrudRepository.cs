namespace InvoiceManagement.ToolBox.Repositories
{
    public interface ICrudRepository<TEntity> where TEntity : class
    {
        bool Any(Func<TEntity, bool> predicate);
        bool Delete(TEntity entity);
        IEnumerable<TEntity> FindMany(Func<TEntity, bool>? predicate = null);
        TEntity? FindOne(Func<TEntity, bool> predicate);
        TEntity? FindOne(params object[] ids);
        TEntity Save(TEntity entity);
        bool Update(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
    }
}