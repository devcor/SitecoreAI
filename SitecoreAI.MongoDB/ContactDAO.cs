using SitecoreAI.Models;

namespace SitecoreAI.MongoDB
{
    public class ContactDAO
    {
        private const string COLLECTION_NAME = "Contacts";

        private string GetFieldValue(string contactId, string field)
        {
            var contact = MongoDAO.GetCollectionItem(COLLECTION_NAME, contactId);
            if (contact == null)
                return string.Empty;

            //this part needs to be refactorized
            var contactAI = contact.GetValue(AIFacet.FacetName);            
            try { return contactAI[field].ToString(); }
            catch { return string.Empty; }
        }

        private void UpdateField(string contactId, string field, string value)
        {
            var contacts = MongoDAO.GetCollection(COLLECTION_NAME);
            MongoDAO.UpdateField(contacts, AIFacet.FacetName + "." + field, value, contactId);
        }

        public string GetAIResult(string contactId)
        {
            return GetFieldValue(contactId, AIFacet._RESULT);
        }

        public string GetAITraining(string contactId)
        {
            return GetFieldValue(contactId, AIFacet._TRAINING);
        }        

        public void SetAIResult(string contactId, string value)
        {
            UpdateField(contactId, AIFacet._RESULT, value);
        }

        public void SetAITraining(string contactId, string value)
        {
            UpdateField(contactId, AIFacet._TRAINING, value);
        }
    }
}
