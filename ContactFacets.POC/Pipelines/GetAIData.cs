using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.ReportingServerDatasource;
using ContactFacets.POC.Models;
using System.Data;

namespace ContactFacets.POC.Pipelines
{
    public class GetAIData : ReportProcessorBase
    {
        public override void Process(ReportProcessorArgs args)
        {
            var queryExpression = this.CreateQuery().Build();
            var table = base.GetTableFromContactQueryExpression(queryExpression, args.ReportParameters.ContactId, null);
            RenameColumns(table);
            args.QueryResult = table;
        }

        protected virtual QueryBuilder CreateQuery()
        {
            var builder = new QueryBuilder
            {
                collectionName = "Contacts"
            };
            builder.Fields.Add("_id");
            builder.Fields.Add(AIContactPersonalInfo.FacetName + "_AIResult");
            builder.Fields.Add(AIContactPersonalInfo.FacetName + "_AITraining");
            builder.QueryParms.Add("_id", "@contactid");
            return builder;
        }

        private void RenameColumns(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
                column.ColumnName = column.ColumnName.Replace(AIContactPersonalInfo.FacetName + "_", "");
        }
    }
}