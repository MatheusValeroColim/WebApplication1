namespace WebApplication1.Services
{
    using MongoDB.Driver;
    using MongoDB.Bson;
    using WebApplication1.Domain;

    public class MongoDBService
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoDBService(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetConnectionString("MongoDB"));
            _database = _client.GetDatabase(configuration["sucodb"]);

        }


        public async Task<List<Suco>> GetAll()
        {
            var collection = _database.GetCollection<Suco>("Sucos");
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Suco> GetById(string id)
        {
            var collection = _database.GetCollection<Suco>("Sucos");
            var objetoId = new ObjectId(id);
            return await collection.Find(x => x.Id == objetoId).FirstOrDefaultAsync();
        }

        public async Task Create(Suco produto)
        {
            var collection = _database.GetCollection<Suco>("Sucos");
            await collection.InsertOneAsync(produto);
        }

        public async Task Update(Suco suco)
        {
            var collection = _database.GetCollection<Suco>("Sucos");
            await collection.ReplaceOneAsync(x => x.Id == suco.Id, suco);
        }

        public async Task Delete(string id)
        {
            var collection = _database.GetCollection<Suco>("Sucos");
            var objetoId = new ObjectId(id);
            await collection.DeleteOneAsync(x => x.Id == objetoId);
        }
    }

}
