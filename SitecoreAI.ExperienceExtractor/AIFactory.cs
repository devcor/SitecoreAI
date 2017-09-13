using ExperienceExtractor.Api.Parsing;
using ExperienceExtractor.Data.Schema;
using ExperienceExtractor.Mapping;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Model.Entities;
using SitecoreAI.Models;

namespace SitecoreAI.ExperienceExtractor
{
    [ParseFactory("aitraining", "Training value", description: "The training value at the end of the visit")]
    public class AIFactory : IParseFactory<IFieldMapper>
    {
        public IFieldMapper Parse(JobParser parser, ParseState state)
        {
            return new SimpleFieldMapper("AITraining", scope => GetTrainingValue(scope.Current<IVisitAggregationContext>().Contact),
                valueType: typeof(string),
                fieldType: FieldType.Dimension);
        }

        private static string GetTrainingValue(IContact contact)
        {
            if (contact == null)
                return string.Empty;

            var aiInfo = contact.GetFacet<IAIFacet>(AIFacet.FacetName);

            if (aiInfo?.Training == null || aiInfo.Training.Length == 0)
                return string.Empty;

            return aiInfo.Training;
        }
    }
}
