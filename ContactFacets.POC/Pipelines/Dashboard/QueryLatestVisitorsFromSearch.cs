using ContactFacets.POC.Models;
using ContactFacets.POC.MongoDB;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using System.Data;

namespace ContactFacets.POC.Pipelines.Dashboard
{
    public class QueryLatestVisitorsFromSearch : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            args.ResultTableForView.Columns.Add(new ViewField<string>("AITraining").ToColumn());
            foreach(DataRow row in args.ResultTableForView.Rows)
            {
                row["AITraining"] = GetTrainingValue(row["ContactId"].ToString());
            }
        }

        private string GetTrainingValue(string id)
        {
            return MongoDAO.GetContactAIDetails(id);
           
        }
    }
}