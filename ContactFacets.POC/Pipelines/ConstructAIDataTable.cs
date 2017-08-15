using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Cintel.Reporting;
using System.Data;

namespace ContactFacets.POC.Pipelines
{
    public class ConstructAIDataTable : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {            
            var columns = new DataColumn[]
            {
                new ViewField<string>("_id").ToColumn(),
                new ViewField<string>("AIResult").ToColumn(),
                new ViewField<string>("AITraining").ToColumn()
            };

            args.ResultTableForView = new DataTable();
            args.ResultTableForView.Columns.AddRange(columns);
        }
    }
}