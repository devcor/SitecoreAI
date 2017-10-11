using Sitecore.Analytics.Data;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using SitecoreAI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SitecoreAI.Pipelines.ExperienceProfile.Dashboard
{
    public class LatestVisitors : ReportProcessorBase
    {
        private readonly ContactRepository _contactRepository;

        public LatestVisitors()
        {
            _contactRepository = Factory.CreateObject("tracking/contactRepository", true) as ContactRepository;
        }

        #region Private Methods

        private string GetLabelsGreaterThan(string currentLabels, double minValue)
        {
            if (string.IsNullOrEmpty(currentLabels))
                return string.Empty;

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
        
        public override void Process(ReportProcessorArgs args)
        {
            if (args.ResultTableForView.Rows.Count == 0)
                return;

            var minLabelValue = double.Parse(ConfigurationManager.AppSettings["MinValueToShowTag"] ?? "80");
            var columnName = AIFacet.FacetName + AIFacet._RESULT;
            args.ResultTableForView.Columns.Add(new ViewField<string>(columnName).ToColumn());

            foreach (DataRow row in args.ResultTableForView.Rows)
            {
                try
                {
                    var contact = _contactRepository.LoadContactReadOnly(new Guid(row["ContactId"].ToString()));

                    if (contact != null)
                    {
                        var ai = contact.GetFacet<IAIFacet>(AIFacet.FacetName);
                        row[columnName] = GetLabelsGreaterThan(ai.Result, minLabelValue);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, this);
                }
            }
        }
    }
}