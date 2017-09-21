using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Interfaces.DAO;
using System;
using System.Collections.Generic;

namespace SitecoreAI.BusinessRules
{
    public class Contact : IContact
    {        
        private readonly IContactDAO _contactDAO;

        public Contact(IContactDAO contactDAO)
        {
            _contactDAO = contactDAO;
        }

        #region Private Methods

        private string GetLabelsGreaterThan(string currentLabels, double minValue)
        {
            if (currentLabels == string.Empty)
                return currentLabels;

            var labels = currentLabels.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            var newLabels = new List<string>();

            foreach (var label in labels)
            {
                var keyValue = label.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length > 1)
                {
                    var success = double.TryParse(keyValue[1], out double value);
                    if (success && value >= minValue)
                        newLabels.Add(keyValue[0]);
                }
            }
            return string.Join(", ", newLabels);
        }

        #endregion

        #region Public Methods

        public string GetAIResult(Guid contactId)
        {
            return _contactDAO.GetAIResult(contactId);
        }

        public string GetAIResult(Guid contactId, double minValue)
        {
            var labels = _contactDAO.GetAIResult(contactId);
            return GetLabelsGreaterThan(labels, minValue);
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
