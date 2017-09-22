using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using System.Data;

namespace SitecoreAI.Pipelines.ExperienceProfile.AI
{
    public class AITableViewFiller : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            var queryResult = args.QueryResult;
            var viewTable = args.ResultTableForView;

            foreach(DataRow row in queryResult.Rows)            
                viewTable.Rows.Add(row.ItemArray);               

            args.ResultSet.Data.Dataset[args.ReportParameters.ViewName] = viewTable;
        }
    }
}