using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Configuration;
using System.Web.Mvc;
using MongoDB.Bson.Serialization;
using System;
using Sitecore.Analytics.Tracking;
using ContactFacets.POC.Models;

namespace ContactFacets.POC.Controllers
{
    public class AIController : Controller
    {
        public ActionResult SaveData(TrainingInfo data)
        {
            var db = GetMongoDB();
            var contacts = db.GetCollection<Contact>("Contacts");
            
            contacts.Update(FilterById(data.Id),
                Update.Set(AIContactPersonalInfo.FacetName + ".AITraining", data.Labels));

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        private QueryDocument FilterById(string id)
        {
            var jsonQuery = "{_id:new BinData(3, '" + EncondeId(id) + "')}";
            BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(jsonQuery);
            return new QueryDocument(document);
        }

        private MongoDatabase GetMongoDB()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["analytics"].ConnectionString;
            var mongoUrl = new MongoUrl(connectionString);
            var server = (new MongoClient(connectionString)).GetServer();
            return server.GetDatabase(mongoUrl.DatabaseName);
        }

        private string EncondeId(string id)
        {
            var guid = Guid.Parse(id);
            var bytes = guid.ToByteArray();
            return Convert.ToBase64String(bytes);
        }

        public class TrainingInfo
        {
            public string Id { get; set; }
            public string Labels { get; set; }
        }
    }
}