using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Cintel.Reporting;
using System.Data;
using SitecoreAI.Models;

namespace SitecoreAI.Pipelines.ExperienceProfile.AI
{
    public class AITableViewDefinition : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {            
            var columns = new DataColumn[]
            {
                new ViewField<string>(AIFacet.FIELD_RESULT).ToColumn(),
                new ViewField<string>(AIFacet.FIELD_TRAINING).ToColumn()
            };

            args.ResultTableForView = new DataTable();
            args.ResultTableForView.Columns.AddRange(columns);
        }
    }
}