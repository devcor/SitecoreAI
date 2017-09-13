using System.Web.Http;
using SitecoreAI.MongoDB;

namespace SitecoreAI.WebApi.Controllers
{
    [RoutePrefix("sitecore/globantai/contact")]
    public class ContactController : ApiController
    {
        private readonly ContactDAO contacts = new ContactDAO();

        [HttpPost]
        [Route("airesult/{id}")]
        public IHttpActionResult Save([FromUri]string id, [FromBody] string aiResult)
        {            
            var response = contacts.SetAIResult(id, aiResult);
            return Ok(new { success = response });
        }

        [HttpGet]
        [Route("airesult/{id}")]
        public string GetAiResult(string id)
        {
            return contacts.GetAIResult(id);
        }

        [HttpGet]
        [Route("aitraining/{id}")]
        public string GetAiTraining(string id)
        {
            return contacts.GetAITraining(id);
        }
    }
}
