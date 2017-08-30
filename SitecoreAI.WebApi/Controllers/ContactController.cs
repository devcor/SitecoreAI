using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SitecoreAI.WebApi.Controllers
{
    [RoutePrefix("sitecore/globantai/contact")]
    public class ContactController : ApiController
    {
        [HttpPost]
        [Route("airesult/{id}")]
        public string Save(string id)
        {
            return "Post airesult";
        }

        [HttpGet]
        [Route("airesult/{id}")]
        public string GetAiResult(string id)
        {
            return "get airesult";
        }

        [HttpGet]
        [Route("aitraining/{id}")]
        public string GetAiTraining(string id)
        {
            return "get aitraining";
        }
    }
}
