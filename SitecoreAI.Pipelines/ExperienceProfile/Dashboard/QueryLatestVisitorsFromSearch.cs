using SitecoreAI.Models;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using System.Data;
using System.Collections.Generic;
using System;
using System.Configuration;
using SitecoreAI.Interfaces.BusinessRules;

namespace SitecoreAI.Pipelines.ExperienceProfile.Dashboard
{
    public class QueryLatestVisitorsFromSearch : ReportProcessorBase
    {
        private const string LabelSeparator = "|";
        private const string ValueSeparator = ":";
        private double MinLabelValue;
        private readonly IContact _contact;

        public QueryLatestVisitorsFromSearch(IContact contact)
        {
            _contact = contact;
        }

        private string GetTrainingValue(string aiResult)
        {
            if (aiResult == string.Empty)
                return aiResult;

            var labels = aiResult.Split(new[] { LabelSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var newLabels = new List<string>();

            foreach (var label in labels)
            {
                var keyValue = label.Split(new[] { ValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length > 1)
                {
                    var success = double.TryParse(keyValue[1], out double value);
                    if (success && value >= MinLabelValue)
                        newLabels.Add(keyValue[0]);
                }
            }
            return string.Join(", ", newLabels);
        }

        public override void Process(ReportProcessorArgs args)
        {
            MinLabelValue = double.Parse(ConfigurationManager.AppSettings["MinValueToShowTag"] ?? "80");
            var columnName = AIFacet.FacetName + AIFacet._RESULT;
            args.ResultTableForView.Columns.Add(new ViewField<string>(columnName).ToColumn());            

            foreach (DataRow row in args.ResultTableForView.Rows)
            {
                var aiResult = _contact.GetAIResult(row["ContactId"].ToString());
                row[columnName] = GetTrainingValue(aiResult);
            }
        }
    }
}