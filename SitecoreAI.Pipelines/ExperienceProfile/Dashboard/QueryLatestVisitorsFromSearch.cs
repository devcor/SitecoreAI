using SitecoreAI.Models;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using System.Data;
using System.Configuration;
using SitecoreAI.Interfaces.BusinessRules;

namespace SitecoreAI.Pipelines.ExperienceProfile.Dashboard
{
    public class QueryLatestVisitorsFromSearch : ReportProcessorBase
    {
        private readonly IContact _contact;

        public QueryLatestVisitorsFromSearch(IContact contact)
        {
            _contact = contact;
        }

        public override void Process(ReportProcessorArgs args)
        {
            if (args.ResultTableForView.Rows.Count == 0)
                return;

            var minLabelValue = double.Parse(ConfigurationManager.AppSettings["MinValueToShowTag"] ?? "80");
            var columnName = AIFacet.FacetName + AIFacet._RESULT;
            args.ResultTableForView.Columns.Add(new ViewField<string>(columnName).ToColumn());            

            foreach (DataRow row in args.ResultTableForView.Rows)
            {
                var aiResult = _contact.GetAIResult(row["ContactId"].ToString());
                row[columnName] = _contact.GetLabelsGreaterThan(aiResult, minLabelValue);
            }
        }
    }
}