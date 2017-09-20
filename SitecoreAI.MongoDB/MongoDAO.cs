using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Configuration;

namespace SitecoreAI.MongoDB
{
    internal class MongoDAO
    {
        private MongoServer _server;
        private MongoUrl _mongoUrl;

        #region Private Methods

        private void Connect()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["analytics"].ConnectionString;
            _mongoUrl = new MongoUrl(connectionString);
            _server = (new MongoClient(connectionString)).GetServer();
        }

        private MongoDatabase GetMongoDB()
        {
            if (_server == null)
                Connect();

            return _server.GetDatabase(_mongoUrl.DatabaseName);
        }        

        private MongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            var db = GetMongoDB();
            return db.GetCollection(collectionName);
        }

        #endregion

        #region Public Methods

        public BsonDocument GetCollectionItem(string collectionName, Guid itemId)
        {
            var query = MongoUtilsDAO.GetQueryById(itemId);
            return GetCollection(collectionName).FindOne(query);
        }

        public WriteConcernResult UpdateField(string collectionName, Guid itemId, string field, string value)
        {
            var query = MongoUtilsDAO.GetQueryById(itemId);
            var collection = GetCollection(collectionName);
            return collection.Update(query, Update.Set(field, value));
        }

        #endregion
    }
}