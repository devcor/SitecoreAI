using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Interfaces.DAO;

namespace SitecoreAI.BusinessRules
{
    public class Contact : IContact
    {
        private readonly IContactDAO _contactDAO;

        public Contact(IContactDAO contactDAO)
        {
            _contactDAO = contactDAO;
        }

        public string GetAIResult(string contactId)
        {
            return _contactDAO.GetAIResult(contactId);
        }

        public string GetAITraining(string contactId)
        {
            return _contactDAO.GetAITraining(contactId);
        }

        public bool SetAIResult(string contactId, string value)
        {
            return _contactDAO.SetAIResult(contactId, value);
        }

        public bool SetAITraining(string contactId, string value)
        {
            return _contactDAO.SetAITraining(contactId, value);
        }
    }
}
