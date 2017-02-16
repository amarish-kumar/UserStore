using System.Configuration;
using MongoDB.Driver;

namespace Training.DAL.Services
{
    public class Context<T> where T : class
    {
        private readonly IMongoDatabase _db;

        protected Context()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["userStoreConnectionString"].ConnectionString;
            var mongoClient = new MongoClient(connectionString);
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            _db = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection(string collectionName)
        {
            return _db.GetCollection<T>(collectionName);
        }
    }
}