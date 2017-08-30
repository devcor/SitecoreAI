﻿using ContactFacets.POC.MongoDB;
using System.Web.Mvc;

namespace ContactFacets.POC.Sitecore.Controllers
{
    public class AIController : Controller
    {
        public ActionResult SaveData(TrainingInfo data)
        {
            MongoDAO.UpdateContact(data.Id, data.Labels);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }        

        public class TrainingInfo
        {
            public string Id { get; set; }
            public string Labels { get; set; }
        }
    }
}