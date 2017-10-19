using SitecoreAI.Interfaces.DAO;
using SitecoreAI.Models;
using System;

namespace SitecoreAI.MongoDB
{
    public class ContactFacetDAO : IContactFacetDAO
    {
        private const string COLLECTION_NAME = "Contacts";
        private MongoDAO _mongoDAO;

        public ContactFacetDAO()
        {
            _mongoDAO = new MongoDAO();
        }

        #region Private Methods

        private string GetFieldValue(Guid contactId, string field)
        {
            var contact = _mongoDAO.GetCollectionItem(COLLECTION_NAME, contactId);
            if (contact == null)
                return "Contact does not exist";

            var AI = contact.GetValue(AIFacet.FacetName, null);
            if (AI == null)
                return "Contact does not contain " + AIFacet.FacetName + " facet";

            return AI.AsBsonDocument.GetValue(field, string.Empty).ToString();
        }

        private bool UpdateField(Guid contactId, string field, string value)
        {
            return _mongoDAO.UpdateField(COLLECTION_NAME, contactId, AIFacet.FacetName + "." + field, value).UpdatedExisting;
        }

        #endregion

        #region Public Methods

        public string GetAIResult(Guid contactId)
        {
            return GetFieldValue(contactId, AIFacet._RESULT);
        }

        public string GetAITraining(Guid contactId)
        {
            return GetFieldValue(contactId, AIFacet._TRAINING);
        }        

        public bool SetAIResult(Guid contactId, string value)
        {
            return UpdateField(contactId, AIFacet._RESULT, value);
        }

        public bool SetAITraining(Guid contactId, string value)
        {
            return UpdateField(contactId, AIFacet._TRAINING, value);
        }

        #endregion
    }
}
