using System.Web.Http;
using SitecoreAI.Interfaces.BusinessRules;
using System;
using SitecoreAI.Models;

namespace SitecoreAI.WebApi.Controllers
{
    [RoutePrefix("sitecore/globantai/contact")]
    public class ContactController : ApiController
    {
        private readonly IContact _contacts;

        public ContactController(IContact contacts)
        {
            _contacts = contacts;
        }

        [HttpPost]
        [Route("{id}/airesult")]
        public IHttpActionResult Save(Guid id, AIModel value)
        {
            if (string.IsNullOrWhiteSpace(value?.Result))
                return BadRequest("AI Result is required.");

            var response = _contacts.SetAIResult(id, value.Result);
            return Ok(new { success = response });
        }

        [HttpGet]
        [Route("{id}/airesult")]
        public string GetAiResult(Guid id)
        {
            return _contacts.GetAIResult(id);
        }

        [HttpGet]
        [Route("{id}/aitraining")]
        public string GetAiTraining(Guid id)
        {
            return _contacts.GetAITraining(id);
        }
    }
}
