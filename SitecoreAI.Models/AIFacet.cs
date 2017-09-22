using Sitecore.Analytics.Model.Framework;
using System;

namespace SitecoreAI.Models
{
    [Serializable]
    public class AIFacet : Facet, IAIFacet
    {
        public static readonly string FacetName = "AI";
        public static readonly string _RESULT = "Result";
        public static readonly string _TRAINING = "Training";

        public AIFacet()
        {
            EnsureAttribute<string>(_RESULT);
            EnsureAttribute<string>(_TRAINING);
        }

        public string Result
        {
            get { return GetAttribute<string>(_RESULT); }
            set { SetAttribute(_RESULT, value); }
        }

        public string Training
        {
            get { return GetAttribute<string>(_TRAINING); }
            set { SetAttribute(_TRAINING, value); }
        }
    }
}