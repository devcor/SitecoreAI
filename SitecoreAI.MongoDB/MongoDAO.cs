using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace SitecoreAI.MongoDB
{
    internal class MongoDAO
    {
        private static MongoDatabase GetMongoDB()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["analytics"].ConnectionString;
            var mongoUrl = new MongoUrl(connectionString);
            var server = (new MongoClient(connectionString)).GetServer();
            return server.GetDatabase(mongoUrl.DatabaseName);
        }      

        private static string EncondeId(string id)
        {
            var guid = Guid.Parse(id);
            var bytes = guid.ToByteArray();
            return Convert.ToBase64String(bytes);
        }

        public static QueryDocument GetQueryById(string id)
        {
            var jsonQuery = "{_id:new BinData(3, '" + EncondeId(id) + "')}";
            BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(jsonQuery);
            return new QueryDocument(document);
        }

        public static MongoCollection<BsonDocument> GetCollection(string name)
        {
            var db = GetMongoDB();
            return db.GetCollection(name);
        }
    }
}