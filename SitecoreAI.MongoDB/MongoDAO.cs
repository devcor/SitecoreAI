using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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

        private static MongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            var db = GetMongoDB();
            return db.GetCollection(collectionName);
        }

        public static BsonDocument GetCollectionItem(string collectionName, string itemId)
        {
            var query = MongoUtilsDAO.GetQueryById(itemId);
            return GetCollection(collectionName).FindOne(query);
        }

        public static WriteConcernResult UpdateField(string collectionName, string itemId, string field, string value)
        {
            var query = MongoUtilsDAO.GetQueryById(itemId);
            var collection = GetCollection(collectionName);

            return collection.Update(query, Update.Set(field, value));
        }
    }
}