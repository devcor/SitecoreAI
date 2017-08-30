using SitecoreAI.Models;
using SitecoreAI.MongoDB;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using System.Data;
using System.Collections.Generic;
using System;

namespace SitecoreAI.Pipelines.ExperienceProfile.Dashboard
{
    public class QueryLatestVisitorsFromSearch : ReportProcessorBase
    {
        private const string LabelSeparator = "|";
        private const string ValueSeparator = ":";
        private const double MinLabelValue = 80;

        private string GetTrainingValue(string aiResult)
        {
            if (aiResult == string.Empty)
                return aiResult;

            var labels = aiResult.Split(new[] { LabelSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var newLabels = new List<string>();

            foreach (var label in labels)
            {
                var value = label.Split(new[] { ValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
                if (double.Parse(value[1].Replace("%", "")) >= MinLabelValue)
                    newLabels.Add(value[0]);
            }
            return string.Join(", ", newLabels);
        }

        public override void Process(ReportProcessorArgs args)
        {
            string columnName = AIFacet.FacetName + AIFacet.FIELD_RESULT;
            args.ResultTableForView.Columns.Add(new ViewField<string>(columnName).ToColumn());

            var contact = new ContactDAO();
            foreach(DataRow row in args.ResultTableForView.Rows)
            {
                var aiResult = contact.GetAIResult(row["ContactId"].ToString());
                row[columnName] = GetTrainingValue(aiResult);
            }
        }
    }
}