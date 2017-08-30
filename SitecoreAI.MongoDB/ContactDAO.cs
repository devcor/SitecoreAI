using System.Linq;
using MongoDB.Driver.Builders;
using SitecoreAI.Models;
using MongoDB.Bson;

namespace SitecoreAI.MongoDB
{
    public class ContactDAO
    {
        private BsonDocument GetContact(string id)
        {
            var query = MongoDAO.GetQueryById(id);
            var contacts = MongoDAO.GetCollection("Contacts");
            var cursor = contacts.Find(query);
            return cursor.SetFields(Fields.Include(AIFacet.FacetName)).ToList().FirstOrDefault();
        }

        private string GetFieldValue(string contactId, string field)
        {
            var doc = GetContact(contactId);
            if (doc == null)
                return string.Empty;

            var ai = doc.GetValue(AIFacet.FacetName);

            var value = string.Empty;
            try { value = ai[field].ToString(); }
            catch { }

            return value;
        }

        private void UpdateField(string contactId, string field, string value)
        {
            var query = MongoDAO.GetQueryById(contactId);
            var contacts = MongoDAO.GetCollection("Contacts");

            contacts.Update(query,
                Update.Set(AIFacet.FacetName + "." + field, value));
        }

        public string GetAIResult(string contactId)
        {
            return GetFieldValue(contactId, AIFacet.FIELD_RESULT);
        }

        public string GetAITraining(string contactId)
        {
            return GetFieldValue(contactId, AIFacet.FIELD_TRAINING);
        }

        public void SetAITraining(string contactId, string value)
        {
            UpdateField(contactId, AIFacet.FIELD_TRAINING, value);
        }

        public void SetAIResult(string contactId, string value)
        {
            UpdateField(contactId, AIFacet.FIELD_RESULT, value);
        }
    }
}
