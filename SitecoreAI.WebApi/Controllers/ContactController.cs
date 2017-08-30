using System.Web.Http;
using SitecoreAI.MongoDB;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SitecoreAI.WebApi.Controllers
{
    [RoutePrefix("sitecore/globantai/contact")]
    public class ContactController : ApiController
    {
        [HttpPost]
        [Route("airesult/{id}")]
        public IEnumerable<string> Save([FromUri]string id, [FromBody] string aiResult)
        {
            var contacts = new ContactDAO();
            contacts.SetAIResult(id, aiResult);
            return new string[] { "Post airesult", id };
        }

        [HttpGet]
        [Route("airesult/{id}")]
        public string GetAiResult(string id)
        {
            var contacts = new ContactDAO();
            return contacts.GetAIResult(id);
        }

        [HttpGet]
        [Route("aitraining/{id}")]
        public string GetAiTraining(string id)
        {
            var contacts = new ContactDAO();
            return contacts.GetAITraining(id);
        }
    }
}
