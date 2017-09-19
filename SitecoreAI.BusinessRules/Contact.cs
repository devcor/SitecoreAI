﻿using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Interfaces.DAO;
using System;
using System.Collections.Generic;

namespace SitecoreAI.BusinessRules
{
    public class Contact : IContact
    {
        private const string LabelSeparator = "|";
        private const string ValueSeparator = ":";
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

        public string GetLabelsGreaterThan(string currentLabels, double minValue)
        {
            if (currentLabels == string.Empty)
                return currentLabels;

            var labels = currentLabels.Split(new[] { LabelSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var newLabels = new List<string>();

            foreach (var label in labels)
            {
                var keyValue = label.Split(new[] { ValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length > 1)
                {
                    var success = double.TryParse(keyValue[1], out double value);
                    if (success && value >= minValue)
                        newLabels.Add(keyValue[0]);
                }
            }
            return string.Join(", ", newLabels);
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
