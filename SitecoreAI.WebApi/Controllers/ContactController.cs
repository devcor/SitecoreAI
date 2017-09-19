﻿using System.Web.Http;
using SitecoreAI.WebApi.Models;
using SitecoreAI.Interfaces.BusinessRules;

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
        public IHttpActionResult Save(string id, AIModel value)
        {
            if (string.IsNullOrWhiteSpace(value?.Result))
                return BadRequest("AI Result is required.");

            var response = _contacts.SetAIResult(id, value.Result);
            return Ok(new { success = response });
        }

        [HttpGet]
        [Route("{id}/airesult")]
        public string GetAiResult(string id)
        {
            return _contacts.GetAIResult(id);
        }

        [HttpGet]
        [Route("{id}/aitraining")]
        public string GetAiTraining(string id)
        {
            return _contacts.GetAITraining(id);
        }
    }
}
