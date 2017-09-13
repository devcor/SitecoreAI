using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace SitecoreAI.MongoDB
{
    internal class MongoUtilsDAO
    {
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
    }
}
