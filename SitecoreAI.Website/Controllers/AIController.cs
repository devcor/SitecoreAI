using System.Web.Mvc;
using SitecoreAI.MongoDB;

namespace SitecoreAI.Website.Controllers
{
    public class AIController : Controller
    {
        public ActionResult SaveData(TrainingInfo data)
        {
            var contact = new ContactDAO();
            contact.SetAITraining(data.Id, data.Labels);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public class TrainingInfo
        {
            public string Id { get; set; }
            public string Labels { get; set; }
        }
    }
}