using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Models;
using System;
using System.Configuration;
using System.Data;

namespace SitecoreAI.Pipelines.ExperienceProfile.Dashboard
{
    public class LatestVisitors : ReportProcessorBase
    {
        private readonly IContactFacet _contact;

        public LatestVisitors(IContactFacet contact)
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
                row[columnName] = _contact.GetAIResult(new Guid(row["ContactId"].ToString()), minLabelValue);            
        }
    }
}