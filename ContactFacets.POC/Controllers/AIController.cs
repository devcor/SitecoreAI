using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ContactFacets.POC.Controllers
{
    public class AIController : Controller
    {
        public ActionResult SaveData(string aiTraining)
        {
            var jss = new JavaScriptSerializer();
            var trainingInfo = jss.Deserialize<TrainingInfo>(aiTraining);

            //Accessing the Server & Database
            //var connectionString = ConfigurationManager.ConnectionStrings["analytics"].ConnectionString;
            //var client = new MongoClient(connectionString);
            //var mongoUrl = new MongoUrl(connectionString);
            //var database = client.GetDatabase(mongoUrl.DatabaseName);
            //var collection = database.GetCollection<dynamic>("Contacts");
                       

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public class TrainingInfo
        {
            public string id { get; set; }
            public string labels { get; set; }
        }
    }
}