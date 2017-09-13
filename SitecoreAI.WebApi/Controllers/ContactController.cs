using System.Web.Http;
using SitecoreAI.MongoDB;
using SitecoreAI.WebApi.Models;

namespace SitecoreAI.WebApi.Controllers
{
    [RoutePrefix("sitecore/globantai/contact")]
    public class ContactController : ApiController
    {
        private readonly ContactDAO contacts = new ContactDAO();

        [HttpPost]
        [Route("{id}/airesult")]
        public IHttpActionResult Save(string id, AIModel value)
        {
            if (string.IsNullOrWhiteSpace(value?.Result))
                return BadRequest("AI Result is required.");

            var response = contacts.SetAIResult(id, value.Result);
            return Ok(new { success = response });
        }

        [HttpGet]
        [Route("{id}/airesult")]
        public string GetAiResult(string id)
        {
            return contacts.GetAIResult(id);
        }

        [HttpGet]
        [Route("{id}/aitraining")]
        public string GetAiTraining(string id)
        {
            return contacts.GetAITraining(id);
        }
    }
}
