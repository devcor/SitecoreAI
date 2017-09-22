using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using SitecoreAI.Models;
using System.Data;

namespace SitecoreAI.Pipelines.ExperienceProfile.AI
{
    public class AITableViewDefinition : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {            
            var columns = new DataColumn[]
            {
                new ViewField<string>(AIFacet._RESULT).ToColumn(),
                new ViewField<string>(AIFacet._TRAINING).ToColumn()
            };

            args.ResultTableForView = new DataTable();
            args.ResultTableForView.Columns.AddRange(columns);
        }
    }
}