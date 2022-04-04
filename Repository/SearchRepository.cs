using Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repository
{
    public class SearchRepository : ISearchRepository
    {
        private IConfiguration configuration;
        public SearchRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<List<DataFormat>> GetData(string searchText, int start)
        {
            try
            {
                var connectionString = configuration.GetSection("ConnectionStrings:TestSearch").Value;

                IMongoClient client = new MongoClient(connectionString);
                IMongoDatabase database = client.GetDatabase("local");
                IMongoCollection<DataFormat> collection = database.GetCollection<DataFormat>("data");

                BsonDocument filter = new BsonDocument();
                filter.Add("$text", new BsonDocument()
                        .Add("$search", searchText)
                );

                BsonDocument sort = new BsonDocument();

                sort.Add("{ score: { $meta: 'textScore' } }", 1.0);

                var options = new FindOptions<DataFormat>()
                {
                    Sort = sort,
                    Skip = start,
                    Limit = 10
                };

                 return collection.FindAsync(filter, options).Result.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<long?> GetCountData(string searchText)
        {
            try
            {
                var connectionString = configuration.GetSection("ConnectionStrings:TestSearch").Value;

                IMongoClient client = new MongoClient(connectionString);
                IMongoDatabase database = client.GetDatabase("local");
                IMongoCollection<DataFormat> collection = database.GetCollection<DataFormat>("data");

                BsonDocument filter = new BsonDocument();
                filter.Add("$text", new BsonDocument()
                        .Add("$search", searchText)
                );

                return collection.Find(filter).Count();//.Result.ToList().Count();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}