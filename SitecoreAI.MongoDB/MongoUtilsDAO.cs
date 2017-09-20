using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace SitecoreAI.MongoDB
{
    internal class MongoUtilsDAO
    {
        #region Private Methods

        private static string EncondeId(Guid id)
        {
            var bytes = id.ToByteArray();
            return Convert.ToBase64String(bytes);
        }

        #endregion

        #region Public Methods

        public static QueryDocument GetQueryById(Guid id)
        {
            var jsonQuery = "{_id:new BinData(3, '" + EncondeId(id) + "')}";
            BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(jsonQuery);
            return new QueryDocument(document);
        }

        #endregion
    }
}
