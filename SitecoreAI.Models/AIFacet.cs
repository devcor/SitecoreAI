using System;
using Sitecore.Analytics.Model.Framework;

namespace SitecoreAI.Models
{
    [Serializable]
    public class AIFacet : Facet, IAIFacet
    {
        public static readonly string FacetName = "AI";
        public static readonly string FIELD_RESULT = "Result";
        public static readonly string FIELD_TRAINING = "Training";

        public AIFacet()
        {
            EnsureAttribute<string>(FIELD_RESULT);
            EnsureAttribute<string>(FIELD_TRAINING);
        }

        public string Result
        {
            get { return GetAttribute<string>(FIELD_RESULT); }
            set { SetAttribute(FIELD_RESULT, value); }
        }

        public string Training
        {
            get { return GetAttribute<string>(FIELD_TRAINING); }
            set { SetAttribute(FIELD_TRAINING, value); }
        }
    }
}