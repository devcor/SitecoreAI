using System.Web.Mvc;
using SitecoreAI.MongoDB;
using SitecoreAI.Website.Models;

namespace SitecoreAI.Website.Controllers
{
    public class AIController : Controller
    {
        private readonly IContacts _contacts;

        public AIController(IContacts contacts)
        {
            _contacts = contacts;
        }

        public ActionResult SaveData(TrainingData data)
        {
            var response = _contacts.SetAITraining(data.ContactId, data.Training);
            return Json(new { success = response }, JsonRequestBehavior.AllowGet);
        }
    }
}