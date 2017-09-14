using SitecoreAI.Models;

namespace SitecoreAI.MongoDB
{
    public class ContactDAO : IContacts
    {
        private const string COLLECTION_NAME = "Contacts";

        private string GetFieldValue(string contactId, string field)
        {
            var contact = MongoDAO.GetCollectionItem(COLLECTION_NAME, contactId);
            if (contact == null)
                return string.Empty;

            var AI = contact.GetValue(AIFacet.FacetName, null);
            if (AI == null)
                return string.Empty;

            return AI.AsBsonDocument.GetValue(field, string.Empty).ToString();
        }

        private bool UpdateField(string contactId, string field, string value)
        {
            return MongoDAO.UpdateField(COLLECTION_NAME, contactId, AIFacet.FacetName + "." + field, value).UpdatedExisting;
        }

        public string GetAIResult(string contactId)
        {
            return GetFieldValue(contactId, AIFacet._RESULT);
        }

        public string GetAITraining(string contactId)
        {
            return GetFieldValue(contactId, AIFacet._TRAINING);
        }        

        public bool SetAIResult(string contactId, string value)
        {
            return UpdateField(contactId, AIFacet._RESULT, value);
        }

        public bool SetAITraining(string contactId, string value)
        {
            return UpdateField(contactId, AIFacet._TRAINING, value);
        }
    }
}
