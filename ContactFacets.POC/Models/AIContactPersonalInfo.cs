using System;
using Sitecore.Analytics.Model.Framework;

namespace ContactFacets.POC.Models
{
    [Serializable]
    public class AIContactPersonalInfo : Facet, IAIContactPersonalInfo
    {
        public static readonly string FacetName = "AIContactPersonalInfo";

        private const string FIELD_AI_RESULT = "AIResult";
        private const string FIELD_AI_TRAINING = "AITraining";

        public AIContactPersonalInfo()
        {
            EnsureAttribute<string>(FIELD_AI_RESULT);
            EnsureAttribute<string>(FIELD_AI_TRAINING);
        }

        public string AIResult
        {
            get { return GetAttribute<string>(FIELD_AI_RESULT); }
            set { SetAttribute(FIELD_AI_RESULT, value); }
        }

        public string AITraining
        {
            get { return GetAttribute<string>(FIELD_AI_TRAINING); }
            set { SetAttribute(FIELD_AI_TRAINING, value); }
        }
    }
}