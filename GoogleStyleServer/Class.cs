using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GoogleStyleServer
{
    public class Class
    {
        public async Task<List<test>> getList(string searchText, int start)
        {
            var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetSection("ConnectionStrings:TestSearch").Value;
            IMongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("local");
            IMongoCollection<test> collection = database.GetCollection<test>("search_data");

            BsonDocument filter = new BsonDocument();
            filter.Add("$text", new BsonDocument()
                    .Add("$search", searchText)
            );

            BsonDocument sort = new BsonDocument();

            sort.Add("{ score: { $meta: 'textScore' } }", 1.0);

            var options = new FindOptions<test>()
            {
                Sort = sort,
                Skip = start,
                Limit = 10
            };

            List<test> rr = collection.FindAsync(filter, options).Result.ToList();

            return rr;
        }
    }

    public class test
    {
        public string _id { get; set; }    
        public string search_text { get; set; }  
    }
}
