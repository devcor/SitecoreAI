using ContactFacets.POC.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sitecore.Analytics.Tracking;
using System;
using System.Configuration;

namespace ContactFacets.POC.MongoDB
{
    public class MongoDAO
    {
        public static string GetContactAIDetails(string id)
        {
            var db = GetMongoDB();
            var query = GetQueryById(id);
            var contacts = db.GetCollection<Contact>("Contacts");
            var val = contacts.Find(query).SetFields(Fields.Include("AI.Details"));

            return val.ToString();
        }

        public static void UpdateContact(string id, string training)
        {
            var db = GetMongoDB();
            var contacts = db.GetCollection<Contact>("Contacts");
            var query = GetQueryById(id);

            contacts.Update(query,
                Update.Set(AIFacet.FacetName + "." + AIFacet.FIELD_TRAINING, training));
        }

        private static MongoDatabase GetMongoDB()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["analytics"].ConnectionString;
            var mongoUrl = new MongoUrl(connectionString);
            var server = (new MongoClient(connectionString)).GetServer();
            return server.GetDatabase(mongoUrl.DatabaseName);
        }

        private static QueryDocument GetQueryById(string id)
        {
            var jsonQuery = "{_id:new BinData(3, '" + EncondeId(id) + "')}";
            BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(jsonQuery);
            return new QueryDocument(document);
        }

        private static string EncondeId(string id)
        {
            var guid = Guid.Parse(id);
            var bytes = guid.ToByteArray();
            return Convert.ToBase64String(bytes);
        }
    }
}