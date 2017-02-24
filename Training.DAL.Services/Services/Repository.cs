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

        public Repository(Context<T> context, string collectionName)
        {
            _collection = context.GetCollection(collectionName);
        }

        public IQueryable<T> GetAll()
        {
            return _collection.AsQueryable();
        }

        public async Task<IQueryable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            var list = await _collection
                .Find(Builders<T>.Filter.Where(predicate))
                .ToListAsync();

            return list.AsQueryable();
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

        public async Task EditAsync(Expression<Func<T, bool>> predicate, T entity)
        {
            await _collection.ReplaceOneAsync(predicate, entity);
        }
    }
}