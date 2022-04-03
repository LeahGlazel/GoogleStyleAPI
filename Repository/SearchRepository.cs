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
            var connectionString = configuration.GetSection("ConnectionStrings:TestSearch").Value;

            IMongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("local");
            IMongoCollection<DataFormat> collection = database.GetCollection<DataFormat>("search_data");

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

            List<DataFormat> rr = collection.FindAsync(filter, options).Result.ToList();

            return rr;
        }
    }
}