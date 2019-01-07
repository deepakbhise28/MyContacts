using System.Collections.Generic;

namespace MyContact.DAL
{
    public interface IRepository<TEntity, in TKey> where TEntity : class 
    {
        IEnumerable<TEntity> Get(string userId);
        TEntity Get(string userId, TKey key);
        bool Insert(string userId, TEntity entity);
        bool Update(string userId, TEntity entity);
        bool Delete(string userId, TKey key);
        void Dispose();
    }
}
