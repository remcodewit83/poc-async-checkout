using Application.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Application.Store
{
    public class MongoDbContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(new MongoUrl(connectionString).DatabaseName);
            Map();
        }

        internal IMongoCollection<Cart> Carts
        {
            get
            {
                return _database.GetCollection<Cart>("Carts");
            }
        }

        private void Map()
        {
            BsonClassMap.RegisterClassMap<Cart>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}
