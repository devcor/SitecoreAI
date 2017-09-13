using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.ConditionalRenderings;
using Sitecore.Rules.Conditions;
using SitecoreAI.Models;
using System;

namespace SitecoreAI.Rules.Conditions
{
    public class ResultMatch<T> : StringOperatorCondition<T> where T : RuleContext
    {
        public string Value { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.IsNotNull(Tracker.Current, "Tracker.Current is not initialized");
            Assert.IsNotNull(Tracker.Current.Contact, "Tracker.Current.Contact is not initialized");
            bool matchFound = false;

            try
            {
                var conditionalRenderingsRuleContext = ruleContext as ConditionalRenderingsRuleContext;
                var datasourceId = conditionalRenderingsRuleContext.Reference.Settings.DataSource;
                var contact = Tracker.Current.Contact;
                var aiFacet = contact.GetFacet<IAIFacet>(AIFacet.FacetName);
                matchFound = aiFacet.Training.ToLower().Contains(Value.ToLower());
            }
            catch (Exception ex)
            {
                Log.Debug(string.Format("ResultMatchRule -- {0}", ex.Message));
            }

            return matchFound;
        }
    }
}
