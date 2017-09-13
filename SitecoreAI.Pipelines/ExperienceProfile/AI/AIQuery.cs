using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.ReportingServerDatasource;
using SitecoreAI.Models;
using System.Data;

namespace SitecoreAI.Pipelines.ExperienceProfile.AI
{
    public class AIQuery : ReportProcessorBase
    {
        private QueryBuilder CreateQuery()
        {
            var builder = new QueryBuilder
            {
                collectionName = "Contacts"
            };

            builder.Fields.Add(AIFacet.FacetName + "." + AIFacet._RESULT);
            builder.Fields.Add(AIFacet.FacetName + "." + AIFacet._TRAINING);
            builder.QueryParms.Add("_id", "@contactid");

            return builder;
        }

        private void RenameColumns(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
                column.ColumnName = column.ColumnName.Replace(AIFacet.FacetName + "_", "");
        }

        public override void Process(ReportProcessorArgs args)
        {
            var queryExpression = CreateQuery().Build();
            var table = GetTableFromContactQueryExpression(queryExpression, args.ReportParameters.ContactId, null);
            RenameColumns(table);
            args.QueryResult = table;
        }
    }
}