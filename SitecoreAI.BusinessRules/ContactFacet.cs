using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Interfaces.DAO;
using System;

namespace SitecoreAI.BusinessRules
{
    public class ContactFacet : IContactFacet
    {
        private readonly IContactFacetDAO _contactDAO;

        public ContactFacet(IContactFacetDAO contactDAO)
        {
            _contactDAO = contactDAO;
        }        

        #region Public Methods

        public string GetAIResult(Guid contactId)
        {
            return _contactDAO.GetAIResult(contactId);
        }

        public string GetAITraining(Guid contactId)
        {
            return _contactDAO.GetAITraining(contactId);
        }

        public bool SetAIResult(Guid contactId, string value)
        {
            return _contactDAO.SetAIResult(contactId, value);
        }

        public bool SetAITraining(Guid contactId, string value)
        {
            return _contactDAO.SetAITraining(contactId, value);
        }

        #endregion
    }
}
