using System.Configuration;
using MongoDB.Driver;

namespace Training.DAL.Services
{
    public class Context
    {
        protected Context()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["userStoreConnectionString"].ConnectionString;
            var mongoClient = new MongoClient(connectionString);
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            Database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoDatabase Database { get; }
    }
}