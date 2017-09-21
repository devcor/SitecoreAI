using Sitecore.Analytics.Data;
using Sitecore.Analytics.Tracking;
using Sitecore.Analytics.Tracking.SharedSessionState;
using Sitecore.Configuration;
using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Interfaces.DAO;
using SitecoreAI.Models;
using System;
using System.Collections.Generic;

namespace SitecoreAI.BusinessRules
{
    public class ContactFacet : IContactFacet
    {
        private ContactRepositoryBase _contactRepository;
        private SharedSessionStateManager _sharedSessionManager;
        private readonly IContactFacetDAO _contactDAO;

        public ContactFacet(IContactFacetDAO contactDAO)
        {
            _contactDAO = contactDAO;
            _contactRepository = Factory.CreateObject("contactRepository", true) as ContactRepositoryBase;
            _sharedSessionManager = Factory.CreateObject("tracking/sharedSessionState/manager", true) as SharedSessionStateManager;
        }

        #region Private Methods

        private Contact GetContactById(Guid contactId)
        {
            // Try to load the contact from the shared session
            var contact = _sharedSessionManager.LockAndLoadContact(contactId);

            if (contact != null)
            {
                _sharedSessionManager.ReleaseContact(contactId);
            }
            else
            {
                //Load contact from database
                contact = _contactRepository.LoadContactReadOnly(contactId);
            }
            return contact;
        }

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

        public string GetAIResult(Guid contactId, double minValue)
        {
            var contact = GetContactById(contactId);
            if (contact == null)
                return string.Empty;

            var aiInfo = contact.GetFacet<IAIFacet>(AIFacet.FacetName);
            if (aiInfo == null || aiInfo.IsEmpty || string.IsNullOrEmpty(aiInfo.Result))
                return string.Empty;

            return GetLabelsGreaterThan(aiInfo.Result, minValue);
        }

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
