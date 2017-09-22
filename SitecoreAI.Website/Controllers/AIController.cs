using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Models;
using System.Web.Mvc;

namespace SitecoreAI.Website.Controllers
{
    public class AIController : Controller
    {
        private readonly IContactFacet _contacts;

        public AIController(IContactFacet contacts)
        {
            _contacts = contacts;
        }

        public ActionResult SaveData(AIModel data)
        {
            var response = _contacts.SetAITraining(data.ContactId, data.Training);
            return Json(new { success = response }, JsonRequestBehavior.AllowGet);
        }
    }
}