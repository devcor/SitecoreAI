using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Cintel.Reporting;
using System.Data;

namespace ContactFacets.POC.Pipelines
{
    public class PopulateWithAIData : ReportProcessorBase
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