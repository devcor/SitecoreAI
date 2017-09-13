using System.Web.Mvc;
using SitecoreAI.MongoDB;
using SitecoreAI.Website.Models;

namespace SitecoreAI.Website.Controllers
{
    public class AIController : Controller
    {
        public ActionResult SaveData(TrainingData data)
        {
            var contact = new ContactDAO();
            var response = contact.SetAITraining(data.ContactId, data.Training);

            return Json(new { success = response }, JsonRequestBehavior.AllowGet);
        }
    }
}