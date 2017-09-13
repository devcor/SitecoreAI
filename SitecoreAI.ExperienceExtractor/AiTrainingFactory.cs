using System;
using ExperienceExtractor.Api.Parsing;
using ExperienceExtractor.Data.Schema;
using ExperienceExtractor.Mapping;
using Sitecore.Analytics.Aggregation.Data.Model;

namespace SitecoreAI.ExperienceExtractor
{
    [ParseFactory("aitraining", "AI Training", "AI Training information for possible client")]
    public class AiTrainingFactory : IParseFactory<IFieldMapper>
    {
        public IFieldMapper Parse(JobParser parser, ParseState state)
        {
            return new SimpleFieldMapper(state.AffixName("aitraining"), scope => scope.Current<IVisitAggregationContext>().TryGet(ctx => ctx.Visit.ContactId), typeof(Guid),
                valueKind: "aitraining",
                calculatedFields: new[]
                {
                    new CalculatedField{Name="Unique visitors", 
                        DaxPattern = string.Format("DISTINCTCOUNT([{0}])", state.AffixName("aitraining")), 
                        ChildDaxPattern = string.Format("CALCULATE(DISTINCTCOUNT(@Parent[{0}]), @TableName)", state.AffixName("aitraining")),
                        FormatString = CalculatedFieldFormat.Integer}
                });
        }
    }
}
