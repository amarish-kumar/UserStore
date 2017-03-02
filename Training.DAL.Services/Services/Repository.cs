using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Training.DAL.Interfaces.Interfaces;

namespace Training.DAL.Services.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(Context context, string collectionName)
        {
            _collection = context.Database.GetCollection<T>(collectionName);
        }

        public IQueryable<T> GetAll()
        {
            return _collection.AsQueryable();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _collection.AsQueryable().Where(predicate);
        }

        public T FindOneBy(Expression<Func<T, bool>> predicate)
        {
            var list = _collection
                .Find(Builders<T>.Filter.Where(predicate))
                .ToList();
            return list.Count == 0 ? default(T) : list[0];
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            await _collection.DeleteOneAsync(predicate);
        }

        public async Task UpdateAsync(Expression<Func<T, bool>> predicate, T entity)
        {
            await _collection.ReplaceOneAsync(predicate, entity);
        }
    }
}