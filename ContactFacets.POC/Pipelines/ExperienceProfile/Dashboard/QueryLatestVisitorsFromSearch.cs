using ContactFacets.POC.Models;
using ContactFacets.POC.MongoDB;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using System.Data;

namespace ContactFacets.POC.Pipelines.ExperienceProfile.Dashboard
{
    public class QueryLatestVisitorsFromSearch : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            string columnName = AIFacet.FacetName + AIFacet.FIELD_RESULT;
            args.ResultTableForView.Columns.Add(new ViewField<string>(columnName).ToColumn());
            foreach(DataRow row in args.ResultTableForView.Rows)
            {
                var aiResult = MongoDAO.GetContactAIResult(row["ContactId"].ToString());
                row[columnName] = TrainingProcessor.GetTrainingValue(aiResult);
            }
        }
    }
}