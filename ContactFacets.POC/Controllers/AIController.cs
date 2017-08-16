using ContactFacets.POC.Models;
using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Data;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            var contactInfo = Tracker.Current.Contact;
            
            var aiInfo = contactInfo.GetFacet<IAIContactPersonalInfo>(AIContactPersonalInfo.FacetName);
            aiInfo.AITraining = trainingInfo.labels;

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public class TrainingInfo
        {
            public string id { get; set; }
            public string labels { get; set; }
        }
    }
}